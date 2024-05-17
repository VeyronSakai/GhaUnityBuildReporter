// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
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

            var buildStep1 = new BuildStep("Test Build Step1", new System.TimeSpan(0, 1, 20, 3, 444),
                Enumerable.Empty<BuildStepMessage>(), 0);
            var buildStep2 = new BuildStep("Test Build Step2", new System.TimeSpan(0, 0, 1, 2, 3),
                new[] { new BuildStepMessage(LogType.Log, "This is Test Step2 Log") }, 1);
            var buildStep3 = new BuildStep("Test Build Step3", new System.TimeSpan(0, 0, 2, 3, 123),
                new[]
                {
                    new BuildStepMessage(LogType.Warning, "This is Test Step3 Warning 1"),
                    new BuildStepMessage(LogType.Warning, "This is Test Step3 Warning 2")
                }, 1);
            var buildStep4 = new BuildStep("Test Build Step4", new System.TimeSpan(0, 0, 30, 10, 111),
                Enumerable.Empty<BuildStepMessage>(), 1);
            var packedAssetInfo = new PackedAssetInfo(15, "Unknown");
            var packedAsset = new PackedAssets("Test Packed Asset", new[] { packedAssetInfo });
            var strippingInfo = new StrippingInfo(new[] { "AndroidJNI Module", "Animation Module", "Audio Module" });
            var buildFile = new BuildFile(0, "TestBuildFile", "TestRole", 100);

            return new BuildReport(summary, new[] { buildStep1, buildStep2, buildStep3, buildStep4 },
                new[] { packedAsset },
                strippingInfo,
                new[] { buildFile });
        }
    }
}
