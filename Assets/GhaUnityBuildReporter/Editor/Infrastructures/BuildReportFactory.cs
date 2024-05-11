// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;
using UnityEditor.Build.Reporting;
using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;
using PackedAssets = GhaUnityBuildReporter.Editor.Domains.PackedAssets;
using StrippingInfo = GhaUnityBuildReporter.Editor.Domains.StrippingInfo;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    public sealed class BuildReportFactory : IBuildReportFactory
    {
        private readonly ILastBuildReportRepository _lastBuildReportRepository;

        public BuildReportFactory(ILastBuildReportRepository lastBuildReportRepository)
        {
            _lastBuildReportRepository = lastBuildReportRepository;
        }

        [CanBeNull]
        public BuildReport CreateBuildReport()
        {
            var originalBuildReport = _lastBuildReportRepository.GetBuildReport();
            if (originalBuildReport == null)
            {
                return null;
            }

            var packedAssetsArray = GetPackedAssetsArray(originalBuildReport);

            var includedModules = originalBuildReport.strippingInfo == null
                ? Array.Empty<string>()
                : originalBuildReport.strippingInfo.includedModules;

            var strippingInfo = new StrippingInfo(includedModules);
            var buildFiles = GetBuildFiles(originalBuildReport);
            return new BuildReport(
                originalBuildReport.summary,
                originalBuildReport.steps,
                packedAssetsArray,
                strippingInfo,
                buildFiles
            );
        }

        private static PackedAssets[] GetPackedAssetsArray(UnityEditor.Build.Reporting.BuildReport originalBuildReport)
        {
            var packedAssets = new PackedAssets[originalBuildReport.packedAssets.Length];
            for (var i = 0; i < originalBuildReport.packedAssets.Length; i++)
            {
                var originalPackedAssets = originalBuildReport.packedAssets[i];
                var packedAssetInfos = new PackedAssetInfo[originalPackedAssets.contents.Length];
                for (var j = 0; j < originalPackedAssets.contents.Length; j++)
                {
                    packedAssetInfos[j] = originalPackedAssets.contents[j];
                }

                var packedAsset = new PackedAssets(originalPackedAssets.shortPath, packedAssetInfos);
                packedAssets[i] = packedAsset;
            }

            return packedAssets;
        }


        private static BuildFile[] GetBuildFiles(UnityEditor.Build.Reporting.BuildReport originalBuildReport)
        {
            return originalBuildReport == null
                ? Array.Empty<BuildFile>()
#if UNITY_2022_1_OR_NEWER
                : originalBuildReport.GetFiles();
#else
                : originalBuildReport.files;
#endif
        }
    }
}
