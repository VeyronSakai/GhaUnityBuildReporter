// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;
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

            var buildSteps = new[]
            {
                new BuildStep("Test Build Step1", new System.TimeSpan(0, 1, 20, 3, 444),
                    Enumerable.Empty<BuildStepMessage>(), 0),
                new BuildStep("Test Build Step2", new System.TimeSpan(0, 0, 1, 2, 3),
                    new[] { new BuildStepMessage(LogType.Log, "This is Test Step2 Log") }, 1),
                new BuildStep("Test Build Step3", new System.TimeSpan(0, 0, 2, 3, 123),
                    new[]
                    {
                        new BuildStepMessage(LogType.Warning, "This is Test Step3 Warning 1"),
                        new BuildStepMessage(LogType.Warning, "This is Test Step3 Warning 2")
                    }, 1),
                new BuildStep("Test Build Step4", new System.TimeSpan(0, 0, 30, 10, 111),
                    Enumerable.Empty<BuildStepMessage>(), 1)
            };

            var packedAssets = new[]
            {
                new PackedAssets("Test Packed Assets A",
                    new[]
                    {
                        new PackedAssetInfo(150000, "Packed Asset A-1"),
                        new PackedAssetInfo(10000, "Packed Asset A-2"),
                    }),
                new PackedAssets("Test Packed Assets B",
                    new[]
                    {
                        new PackedAssetInfo(20000, "Packed Asset B-1"),
                        new PackedAssetInfo(300000000, "Packed Asset B-2"),
                        new PackedAssetInfo(250, "Packed Asset B-3"),
                    })
            };


            var strippingInfo = new StrippingInfo(new[] { "AndroidJNI Module", "Animation Module", "Audio Module" });
            var buildFiles = new[]
            {
                new BuildFile(0, "TestBuildFile1", "Test Role A", 100),
                new BuildFile(1, "TestBuildFile2", "Test Role B", 2000),
                new BuildFile(2, "TestBuildFile3", "Test Role C", 30000)
            };

            return new BuildReport(
                summary,
                buildSteps,
                packedAssets,
                strippingInfo,
                buildFiles);
        }

        internal static string GetExpectedResult(string expectedResultFileName)
        {
            var expectedResultPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName,
                "Tests",
                "Editor",
                "TestData",
                expectedResultFileName
            );
            return File.ReadAllText(expectedResultPath);
        }
    }
}
