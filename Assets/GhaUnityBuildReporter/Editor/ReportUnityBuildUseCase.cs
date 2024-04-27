// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor
{
    internal sealed class ReportUnityBuildUseCase
    {
        private readonly IJobSummaryRepository _jobSummaryRepository;
        private readonly BuildReport _buildReport;

        private CancellationTokenSource _cts = new();

        public ReportUnityBuildUseCase(IJobSummaryRepository jobSummaryRepository, BuildReport buildReport)
        {
            _jobSummaryRepository = jobSummaryRepository;
            _buildReport = buildReport;
        }

        public async Task WriteAllAsync()
        {
            var token = _cts.Token;
            await WriteTitleAsync(token);
            await WriteSummaryAsync(token);
            await WriteBuildStepsInfoAsync(token);
            await WriteSourceAssetsInfoAsync(token);
            await WriteOutputFilesInfoAsync(token);
            await WriteIncludedModulesInfoAsync(token);
        }

        private async Task WriteTitleAsync(CancellationToken cancellationToken)
        {
            await _jobSummaryRepository.AppendTextAsync($"# Unity Build Report{Environment.NewLine}",
                cancellationToken);
        }

        private async Task WriteSummaryAsync(CancellationToken cancellationToken)
        {
            await _jobSummaryRepository.AppendTextAsync($"## Basic Info{Environment.NewLine}", cancellationToken);

            var summary = _buildReport.summary;

            var basicInfo =
                $"| Key | Value |{Environment.NewLine}"
                + $"| --- | --- |{Environment.NewLine}"
                + $"| Platform | {summary.platform} |{Environment.NewLine}"
                + $@"| Total Time | {summary.totalTime:hh\:mm\:ss\.fff}|{Environment.NewLine}"
                + $"| Total Size | {GetFormattedSize(summary.totalSize)} |{Environment.NewLine}"
                + $"| Build Result | {summary.result} |{Environment.NewLine}"
                + $"| Total Errors | {summary.totalErrors} |{Environment.NewLine}"
                + $"| Total Warnings | {summary.totalWarnings} |{Environment.NewLine}";

            await _jobSummaryRepository.AppendTextAsync(basicInfo, cancellationToken);
        }

        private async Task WriteBuildStepsInfoAsync(CancellationToken cancellationToken)
        {
            if (_buildReport.steps.Length <= 0)
            {
                return;
            }

            await _jobSummaryRepository.AppendTextAsync($"## Build Steps{Environment.NewLine}", cancellationToken);

            await _jobSummaryRepository.AppendTextAsync(
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}", cancellationToken);

            foreach (var step in _buildReport.steps)
            {
                switch (step.depth)
                {
                    case 0:
                        await _jobSummaryRepository.AppendTextAsync(
                            $@"### {step.name} ({step.duration:hh\:mm\:ss\.fff}){Environment.NewLine}",
                            cancellationToken);
                        break;
                    case >= 1:
                        {
                            await _jobSummaryRepository.AppendTextAsync(
                                $@"{new string(' ', (step.depth - 1) * 2)}- **{step.name}** ({step.duration:hh\:mm\:ss\.fff}){Environment.NewLine}",
                                cancellationToken);

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

                                await _jobSummaryRepository.AppendTextAsync(
                                    $@"{new string(' ', step.depth * 2)}- {emoji} {message.content}{Environment.NewLine}",
                                    cancellationToken);
                            }

                            break;
                        }
                }
            }

            await _jobSummaryRepository.AppendTextAsync($"</details>{Environment.NewLine}{Environment.NewLine}",
                cancellationToken);
        }

        private async Task WriteSourceAssetsInfoAsync(CancellationToken cancellationToken)
        {
            if (!_buildReport.packedAssets.Any())
            {
                return;
            }

            await _jobSummaryRepository.AppendTextAsync($"## Source Assets{Environment.NewLine}", cancellationToken);

            foreach (var packedAsset in _buildReport.packedAssets)
            {
                var totalSize = packedAsset.contents.Aggregate<PackedAssetInfo, ulong>(0,
                    (current, packedAssetContent) => current + packedAssetContent.packedSize);

                var topAssets = packedAsset.contents.OrderByDescending(x => x.packedSize);

                await _jobSummaryRepository.AppendTextAsync(
                    $"### {packedAsset.shortPath} ({GetFormattedSize(totalSize)}){Environment.NewLine}",
                    cancellationToken);

                await _jobSummaryRepository.AppendTextAsync(
                    $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}",
                    cancellationToken);

                await _jobSummaryRepository.AppendTextAsync(
                    $"| Asset | Size |{Environment.NewLine}| --- | --- |{Environment.NewLine}", cancellationToken);

                foreach (var assetInfo in topAssets)
                {
                    var assetPath = string.IsNullOrEmpty(assetInfo.sourceAssetPath)
                        ? "Unknown"
                        : assetInfo.sourceAssetPath;

                    var assetDetails =
                        $"| {assetPath} | {GetFormattedSize(assetInfo.packedSize)} |{Environment.NewLine}";

                    await _jobSummaryRepository.AppendTextAsync(assetDetails, cancellationToken);
                }

                await _jobSummaryRepository.AppendTextAsync($"</details>{Environment.NewLine}{Environment.NewLine}",
                    cancellationToken);
            }
        }

        private async Task WriteOutputFilesInfoAsync(CancellationToken cancellationToken)
        {
            var files = GetBuildFiles();
            if (files.Length == 0)
            {
                return;
            }

            await _jobSummaryRepository.AppendTextAsync($"## Output Files{Environment.NewLine}", cancellationToken);

            await _jobSummaryRepository.AppendTextAsync(
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}", cancellationToken);

            await _jobSummaryRepository.AppendTextAsync($"| File | Size |{Environment.NewLine}"
                                                        + $"| --- | --- |{Environment.NewLine}", cancellationToken);

            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;

            foreach (var file in files)
            {
                var relativePath = Path.GetRelativePath(projectRootPath, file.path);
                await _jobSummaryRepository.AppendTextAsync(
                    $"| {relativePath} | {GetFormattedSize(file.size)} |{Environment.NewLine}", cancellationToken);
            }

            await _jobSummaryRepository.AppendTextAsync($"</details>{Environment.NewLine}{Environment.NewLine}",
                cancellationToken);
        }

        private async Task WriteIncludedModulesInfoAsync(CancellationToken cancellationToken)
        {
            if (_buildReport.strippingInfo == null)
            {
                return;
            }

            await _jobSummaryRepository.AppendTextAsync($"## Included Modules{Environment.NewLine}", cancellationToken);

            await _jobSummaryRepository.AppendTextAsync(
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}", cancellationToken);

            foreach (var item in _buildReport.strippingInfo.includedModules)
            {
                await WriteIncludedModuleInfoInternalAsync(item, 0, cancellationToken);
            }

            await _jobSummaryRepository.AppendTextAsync($"</details>{Environment.NewLine}{Environment.NewLine}",
                cancellationToken);
        }

        private async Task WriteIncludedModuleInfoInternalAsync(string item, uint depth,
            CancellationToken cancellationToken)
        {
            await _jobSummaryRepository.AppendTextAsync(depth == 0
                ? $@"- **{item}**{Environment.NewLine}"
                : $@"{new string(' ', (int)(depth * 2))} - {item}{Environment.NewLine}", cancellationToken);

            var reasons = _buildReport.strippingInfo.GetReasonsForIncluding(item);
            foreach (var reason in reasons)
            {
                await WriteIncludedModuleInfoInternalAsync(reason, depth + 1, cancellationToken);
            }
        }

        private BuildFile[] GetBuildFiles()
        {
#if UNITY_2022_1_OR_NEWER
            return _buildReport.GetFiles();
#else
            return _buildReport.files;
#endif
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
