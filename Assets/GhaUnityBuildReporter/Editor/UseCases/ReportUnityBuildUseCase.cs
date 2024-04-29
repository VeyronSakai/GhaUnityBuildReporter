// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.IO;
using System.Linq;
using GhaUnityBuildReporter.Editor.Domains;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    public sealed class ReportUnityBuildUseCase
    {
        private readonly IJobSummaryRepository _jobSummaryRepository;
        private readonly IBuildReportRepository _buildReportRepository;

        public ReportUnityBuildUseCase(
            IJobSummaryRepository jobSummaryRepository,
            IBuildReportRepository buildReportRepository
        )
        {
            _jobSummaryRepository = jobSummaryRepository;
            _buildReportRepository = buildReportRepository;
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
        }

        private void WriteSummary()
        {
            _jobSummaryRepository.AppendText($"## Basic Info{Environment.NewLine}");

            var summary = _buildReportRepository.GetBuildSummary();

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
            var steps = _buildReportRepository.GetBuildSteps();
            if (steps.Length <= 0)
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Build Steps{Environment.NewLine}");

            _jobSummaryRepository.AppendText(
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}");

            foreach (var step in steps)
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
            var packedAssetsCount = _buildReportRepository.GetPackedAssetsCount();
            if (packedAssetsCount == 0)
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Source Assets{Environment.NewLine}");

            for (var i = 0; i < packedAssetsCount; i++)
            {
                var totalSize = _buildReportRepository.GetPackedAssetSize(i);

                var packedAssets = _buildReportRepository.GetPackedAssetContents(i)
                    .OrderByDescending(x => x.packedSize);

                _jobSummaryRepository.AppendText(
                    $"### {_buildReportRepository.GetPackAssetShortPath(i)} ({GetFormattedSize(totalSize)}){Environment.NewLine}");

                _jobSummaryRepository.AppendText(
                    $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}");

                _jobSummaryRepository.AppendText(
                    $"| File | Size |{Environment.NewLine}| --- | --- |{Environment.NewLine}");

                foreach (var packedAsset in packedAssets)
                {
                    var assetPath = string.IsNullOrEmpty(packedAsset.sourceAssetPath)
                        ? "Unknown"
                        : packedAsset.sourceAssetPath;

                    var assetDetails =
                        $"| {assetPath} | {GetFormattedSize(packedAsset.packedSize)} |{Environment.NewLine}";

                    _jobSummaryRepository.AppendText(assetDetails);
                }

                _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
            }
        }

        private void WriteOutputFilesInfo()
        {
            var buildFiles = _buildReportRepository.GetBuildFiles();
            if (!buildFiles.Any())
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Output Files{Environment.NewLine}");

            _jobSummaryRepository.AppendText(
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}");

            _jobSummaryRepository.AppendText($"| File | Size |{Environment.NewLine}"
                                             + $"| --- | --- |{Environment.NewLine}");

            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;

            foreach (var file in buildFiles)
            {
                var relativePath = Path.GetRelativePath(projectRootPath, file.path);
                _jobSummaryRepository.AppendText(
                    $"| {relativePath} | {GetFormattedSize(file.size)} |{Environment.NewLine}");
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }

        private void WriteIncludedModulesInfo()
        {
            var includedModuleNames = _buildReportRepository.GetIncludedModuleNames().ToArray();
            if (!includedModuleNames.Any())
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Included Modules{Environment.NewLine}");

            _jobSummaryRepository.AppendText(
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}");

            foreach (var entity in includedModuleNames)
            {
                WriteIncludedModuleInfoInternal(entity, 0);
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }

        private void WriteIncludedModuleInfoInternal(string entity, uint depth)
        {
            _jobSummaryRepository.AppendText(depth == 0
                ? $@"- **{entity}**{Environment.NewLine}"
                : $@"{new string(' ', (int)(depth * 2))} - {entity}{Environment.NewLine}");


            var reasons = _buildReportRepository.GetReasonsForIncluding(entity);
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
