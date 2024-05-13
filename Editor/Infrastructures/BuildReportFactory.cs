// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Linq;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;
using UnityEditor.Build.Reporting;
using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;
using BuildStep = GhaUnityBuildReporter.Editor.Domains.BuildStep;
using BuildStepMessage = GhaUnityBuildReporter.Editor.Domains.BuildStepMessage;
using BuildSummary = GhaUnityBuildReporter.Editor.Domains.BuildSummary;
using PackedAssetInfo = GhaUnityBuildReporter.Editor.Domains.PackedAssetInfo;
using PackedAssets = GhaUnityBuildReporter.Editor.Domains.PackedAssets;
using StrippingInfo = GhaUnityBuildReporter.Editor.Domains.StrippingInfo;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal sealed class BuildReportFactory : AbstractBuildReportFactory
    {
        internal override BuildReport CreateBuildReport(UnityEditor.Build.Reporting.BuildReport originalBuildReport)
        {
            var packedAssetsArray = GetPackedAssetsArray(originalBuildReport);

            var includedModules = originalBuildReport.strippingInfo == null
                ? Array.Empty<string>()
                : originalBuildReport.strippingInfo.includedModules;

            var strippingInfo = new StrippingInfo(includedModules);
            var buildFiles = GetBuildFiles(originalBuildReport);

            var summary = new BuildSummary(
                originalBuildReport.summary.platform,
                originalBuildReport.summary.totalTime,
                originalBuildReport.summary.totalSize,
                originalBuildReport.summary.result,
                originalBuildReport.summary.totalErrors,
                originalBuildReport.summary.totalWarnings
            );

            var buildSteps = originalBuildReport.steps.Select(originalBuildStep =>
            {
                var messages = originalBuildStep.messages.Select(originalMessage =>
                    new BuildStepMessage(originalMessage.type, originalMessage.content));

                return new BuildStep(originalBuildStep.name, originalBuildStep.duration, messages,
                    originalBuildStep.depth);
            }).ToArray();

            return new BuildReport(
                summary,
                buildSteps,
                packedAssetsArray,
                strippingInfo,
                buildFiles
            );
        }

        [NotNull]
        private static PackedAssets[] GetPackedAssetsArray(
            [NotNull] UnityEditor.Build.Reporting.BuildReport originalBuildReport)
        {
            var packedAssets = new PackedAssets[originalBuildReport.packedAssets.Length];
            for (var i = 0; i < originalBuildReport.packedAssets.Length; i++)
            {
                var originalPackedAssets = originalBuildReport.packedAssets[i];
                var packedAssetInfos = new PackedAssetInfo[originalPackedAssets.contents.Length];
                for (var j = 0; j < originalPackedAssets.contents.Length; j++)
                {
                    packedAssetInfos[j] = new PackedAssetInfo(originalPackedAssets.contents[j].packedSize,
                        originalPackedAssets.contents[j].sourceAssetPath);
                }

                var packedAsset = new PackedAssets(originalPackedAssets.shortPath, packedAssetInfos);
                packedAssets[i] = packedAsset;
            }

            return packedAssets;
        }

        [NotNull]
        private static BuildFile[] GetBuildFiles([NotNull] UnityEditor.Build.Reporting.BuildReport originalBuildReport)
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
