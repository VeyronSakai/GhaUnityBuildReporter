// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;
using GhaUnityBuildReporter.Editor.Tests.TestDoubles;
using GhaUnityBuildReporter.Editor.UseCases;
using NUnit.Framework;

namespace GhaUnityBuildReporter.Editor.Tests
{
    public sealed class UnityBuildReportWriterTest
    {
        private readonly string _outputPath =
            Path.Combine(Directory.GetCurrentDirectory(), "ActualUnityBuildReport.md");

        [Test]
        public void Write_ConfigDoesNotExist_AllInfoIsWritten()
        {
            // Arrange
            var jobSummaryRepository = new FakeJobSummaryRepository(_outputPath);
            var buildReportRepository = new StubBuildReportRepository();
            var reportConfigRepository = new StubReportConfigRepository(existsConfig: false);
            var writer =
                new UnityBuildReportWriter(jobSummaryRepository, buildReportRepository, reportConfigRepository);

            // Act
            writer.Write();

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expected = Helper.GetExpectedResult("ExpectedUnityBuildReport.md");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Write_OnlyTitleAndBasicInfoAreEnabled_OnlyTitleAndBasicInfoAreWritten()
        {
            // Arrange
            var jobSummaryRepository = new FakeJobSummaryRepository(_outputPath);
            var buildReportRepository = new StubBuildReportRepository();
            var reportConfigRepository = new StubReportConfigRepository(
                writesTitle: true,
                writesBasicInfo: true,
                writesBuildSteps: false,
                writesSourceAssets: false,
                writesOutputFiles: false,
                writesIncludedModules: false
            );
            var writer =
                new UnityBuildReportWriter(jobSummaryRepository, buildReportRepository, reportConfigRepository);

            // Act
            writer.Write();

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expected = Helper.GetExpectedResult("ExpectedUnityBuildReport_OnlyTitleAndBasicInfoAreWritten.md");
            Assert.AreEqual(expected, actual);
        }

        [SetUp, TearDown]
        public void Clean()
        {
            if (File.Exists(_outputPath))
            {
                File.Delete(_outputPath);
            }
        }
    }
}
