// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.IO;
using System.Linq;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;
using UnityEditor.Build.Reporting;
using UnityEngine;
using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class UnityBuildReporter
    {
        [CanBeNull] private readonly BuildReport _buildReport;
        private readonly IJobSummaryRepository _jobSummaryRepository;
        private readonly ILastBuildReportRepository _lastBuildReportRepository;

        public UnityBuildReporter(
            IJobSummaryRepository jobSummaryRepository,
            ILastBuildReportRepository lastBuildReportRepository,
            IBuildReportFactory buildReportFactory
        )
        {
            _jobSummaryRepository = jobSummaryRepository;
            _lastBuildReportRepository = lastBuildReportRepository;
            _buildReport = buildReportFactory.CreateBuildReport();
        }

        public void WriteAll()
        {
            WriteTitle();
            WriteSummary();
            WriteBuildStepsInfo();
            WriteSourceAssetsInfo();
            WriteOutputFilesInfo();
            WriteIncludedModulesInfo();
        }

        private void WriteTitle()
        {
            _jobSummaryRepository.AppendText($"# Unity Build Report{Environment.NewLine}");
            if (_buildReport == null)
            {
                _jobSummaryRepository.AppendText($"Build report not found {Environment.NewLine}");
            }
        }

        private void WriteSummary()
        {
            if (_buildReport == null)
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Basic Info{Environment.NewLine}");

            var summary = _buildReport.Summary;

            var basicInfo =
                $"| Key | Value |{Environment.NewLine}"
                + $"| --- | --- |{Environment.NewLine}"
                + $"| Platform | {summary.platform} |{Environment.NewLine}"
                + $@"| Total Time | {summary.totalTime:hh\:mm\:ss\.fff}|{Environment.NewLine}"
                + $"| Total Size | {GetFormattedSize(summary.totalSize)} |{Environment.NewLine}"
                + $"| Build Result | {summary.result} |{Environment.NewLine}"
                + $"| Total Errors | {summary.totalErrors} |{Environment.NewLine}"
                + $"| Total Warnings | {summary.totalWarnings} |{Environment.NewLine}";

            _jobSummaryRepository.AppendText(basicInfo);
        }

        private void WriteBuildStepsInfo()
        {
            if (_buildReport == null || _buildReport.Steps.Length <= 0)
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Build Steps{Environment.NewLine}" +
                                             $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}");

            foreach (var step in _buildReport.Steps)
            {
                switch (step.depth)
                {
                    case 0:
                        _jobSummaryRepository.AppendText(
                            $@"### {step.name} ({step.duration:hh\:mm\:ss\.fff}){Environment.NewLine}");
                        break;
                    case >= 1:
                        {
                            _jobSummaryRepository.AppendText(
                                $@"{new string(' ', (step.depth - 1) * 2)}- **{step.name}** ({step.duration:hh\:mm\:ss\.fff}){Environment.NewLine}");

                            if (step.messages.Length <= 0)
                            {
                                continue;
                            }

                            foreach (var message in step.messages)
                            {
                                var emoji = message.type switch
                                {
                                    LogType.Error => ":x:",
                                    LogType.Assert => ":no_entry_sign:",
                                    LogType.Warning => ":warning:",
                                    LogType.Log => ":information_source:",
                                    LogType.Exception => ":boom:",
                                    _ => ":question:"
                                };

                                _jobSummaryRepository.AppendText(
                                    $@"{new string(' ', step.depth * 2)}- {emoji} {message.content}{Environment.NewLine}");
                            }

                            break;
                        }
                }
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }

        private void WriteSourceAssetsInfo()
        {
            if (_buildReport == null || !_buildReport.PackedAssets.Any())
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Source Assets{Environment.NewLine}");

            foreach (var packedAsset in _buildReport.PackedAssets)
            {
                var totalSize = packedAsset.Contents.Aggregate<PackedAssetInfo, ulong>(0,
                    (current, packedAssetContent) => current + packedAssetContent.packedSize);

                var topAssets = packedAsset.Contents.OrderByDescending(x => x.packedSize);

                _jobSummaryRepository.AppendText(
                    $"### {packedAsset.ShortPath} ({GetFormattedSize(totalSize)}){Environment.NewLine}" +
                    $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}" +
                    $"| File | Size |{Environment.NewLine}| --- | --- |{Environment.NewLine}"
                );

                foreach (var assetInfo in topAssets)
                {
                    var assetPath = string.IsNullOrEmpty(assetInfo.sourceAssetPath)
                        ? "Unknown"
                        : assetInfo.sourceAssetPath;

                    var assetDetails =
                        $"| {assetPath} | {GetFormattedSize(assetInfo.packedSize)} |{Environment.NewLine}";

                    _jobSummaryRepository.AppendText(assetDetails);
                }

                _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
            }
        }

        private void WriteOutputFilesInfo()
        {
            if (_buildReport == null || _buildReport.BuildFiles.Length == 0)
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Output Files{Environment.NewLine}" +
                                             $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}" +
                                             $"| File | Size |{Environment.NewLine}" +
                                             $"| --- | --- |{Environment.NewLine}");

            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;

            foreach (var file in _buildReport.BuildFiles)
            {
                var relativePath = Path.GetRelativePath(projectRootPath, file.path);
                _jobSummaryRepository.AppendText(
                    $"| {relativePath} | {GetFormattedSize(file.size)} |{Environment.NewLine}");
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }

        private void WriteIncludedModulesInfo()
        {
            if (_buildReport?.StrippingInfo == null || !_buildReport.StrippingInfo.IncludedModules.Any())
            {
                return;
            }

            _jobSummaryRepository.AppendText(
                $"## Included Modules{Environment.NewLine}" +
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}"
            );

            foreach (var item in _buildReport.StrippingInfo.IncludedModules)
            {
                WriteIncludedModuleInfoInternal(item, 0);
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }

        private void WriteIncludedModuleInfoInternal(string item, uint depth)
        {
            _jobSummaryRepository.AppendText(depth == 0
                ? $@"- **{item}**{Environment.NewLine}"
                : $@"{new string(' ', (int)(depth * 2))} - {item}{Environment.NewLine}");

            if (_buildReport == null)
            {
                return;
            }

            var reasons = _lastBuildReportRepository.GetReasonsForIncluding(item);
            foreach (var reason in reasons)
            {
                WriteIncludedModuleInfoInternal(reason, depth + 1);
            }
        }

        private static string GetFormattedSize(ulong size)
        {
            return size switch
            {
                < 1024 => size + " B",
                < 1024 * 1024 => (size / 1024.00).ToString("F2") + " KB",
                < 1024 * 1024 * 1024 => (size / (1024.0 * 1024.0)).ToString("F2") + " MB",
                _ => (size / (1024.0 * 1024.0 * 1024.0)).ToString("F2") + " GB"
            };
        }
    }
}
