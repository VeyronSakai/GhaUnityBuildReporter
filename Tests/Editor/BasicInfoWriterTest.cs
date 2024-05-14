// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;
using System.Linq;
using GhaUnityBuildReporter.Editor.Tests.TestDoubles;
using GhaUnityBuildReporter.Editor.UseCases;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Build.Reporting;
using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;
using BuildStep = GhaUnityBuildReporter.Editor.Domains.BuildStep;
using BuildStepMessage = GhaUnityBuildReporter.Editor.Domains.BuildStepMessage;
using BuildSummary = GhaUnityBuildReporter.Editor.Domains.BuildSummary;
using StrippingInfo = GhaUnityBuildReporter.Editor.Domains.StrippingInfo;
using PackedAssetInfo = GhaUnityBuildReporter.Editor.Domains.PackedAssetInfo;
using PackedAssets = GhaUnityBuildReporter.Editor.Domains.PackedAssets;
using BuildFile = GhaUnityBuildReporter.Editor.Domains.BuildFile;

namespace GhaUnityBuildReporter.Editor.Tests
{
    public sealed class BasicInfoWriterTest
    {
        private readonly string _outputPath =
            Path.Combine(Directory.GetCurrentDirectory(), "ActualBasicInfo.md");

        [Test]
        public void WriteTest()
        {
            // Arrange
            var jobSummaryRepository = new SpyJobSummaryRepository(_outputPath);
            var writer = new BasicInfoWriter(jobSummaryRepository);
            var buildReport = GenerateMockBuildReport();

            // Act
            writer.Write(buildReport);

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expectedResultPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName,
                "Tests",
                "Editor",
                "TestData",
                "ExpectedBasicInfo.md"
            );
            var expected = File.ReadAllText(expectedResultPath);
            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_outputPath))
            {
                File.Delete(_outputPath);
            }
        }

        private static BuildReport GenerateMockBuildReport()
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
