// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Linq;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;
using UnityEditor.Build.Reporting;
using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class SourceAssetsWriter
    {
        private readonly AbstractJobSummaryRepository _jobSummaryRepository;

        internal SourceAssetsWriter([NotNull] AbstractJobSummaryRepository jobSummaryRepository)
        {
            _jobSummaryRepository = jobSummaryRepository;
        }

        internal void Write([NotNull] BuildReport buildReport)
        {
            if (!buildReport.PackedAssets.Any())
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Source Assets{Environment.NewLine}");

            foreach (var packedAsset in buildReport.PackedAssets)
            {
                var totalSize = packedAsset.Contents.Aggregate<PackedAssetInfo, ulong>(0,
                    (current, packedAssetContent) => current + packedAssetContent.packedSize);

                var topAssets = packedAsset.Contents.OrderByDescending(x => x.packedSize);

                _jobSummaryRepository.AppendText(
                    $"### {packedAsset.ShortPath} ({SizeFormatter.GetFormattedSize(totalSize)}){Environment.NewLine}" +
                    $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}" +
                    $"| File | Size |{Environment.NewLine}| --- | --- |{Environment.NewLine}"
                );

                foreach (var assetInfo in topAssets)
                {
                    var assetPath = string.IsNullOrEmpty(assetInfo.sourceAssetPath)
                        ? "Unknown"
                        : assetInfo.sourceAssetPath;

                    var assetDetails =
                        $"| {assetPath} | {SizeFormatter.GetFormattedSize(assetInfo.packedSize)} |{Environment.NewLine}";

                    _jobSummaryRepository.AppendText(assetDetails);
                }

                _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
            }
        }
    }
}
