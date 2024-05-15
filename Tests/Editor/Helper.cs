// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using BuildFile = GhaUnityBuildReporter.Editor.Domains.BuildFile;
using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;
using BuildStep = GhaUnityBuildReporter.Editor.Domains.BuildStep;
using BuildStepMessage = GhaUnityBuildReporter.Editor.Domains.BuildStepMessage;
using BuildSummary = GhaUnityBuildReporter.Editor.Domains.BuildSummary;
using PackedAssetInfo = GhaUnityBuildReporter.Editor.Domains.PackedAssetInfo;
using PackedAssets = GhaUnityBuildReporter.Editor.Domains.PackedAssets;
using StrippingInfo = GhaUnityBuildReporter.Editor.Domains.StrippingInfo;

namespace GhaUnityBuildReporter.Editor.Tests
{
    internal class Helper
    {
        internal static BuildReport GenerateStubBuildReport()
        {
            var summary = new BuildSummary(
                BuildTarget.Android,
                new System.TimeSpan(0, 1, 2, 3, 4),
                1000,
                BuildResult.Succeeded,
                1,
                2
            );

            var buildStep = new BuildStep("Test Step", new System.TimeSpan(0, 0, 1, 2, 3),
                Enumerable.Empty<BuildStepMessage>(), 0);
            var packedAssetInfo = new PackedAssetInfo(15, "Unknown");
            var packedAsset = new PackedAssets("Test Packed Asset", new[] { packedAssetInfo });
            var strippingInfo = new StrippingInfo(new[] { "Test Included Module" });
            var buildFile = new BuildFile(0, "TestBuildFile", "TestRole", 100);

            return new BuildReport(summary, new[] { buildStep }, new[] { packedAsset }, strippingInfo,
                new[] { buildFile });
        }
    }
}
