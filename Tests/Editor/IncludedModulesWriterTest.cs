// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;
using GhaUnityBuildReporter.Editor.Tests.TestDoubles;
using GhaUnityBuildReporter.Editor.UseCases;
using NUnit.Framework;

namespace GhaUnityBuildReporter.Editor.Tests
{
    public sealed class IncludedModulesWriterTest
    {
        private readonly string _outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ActualIncludedModules.md");

        [Test]
        public void WriteTest()
        {
            // Arrange
            var jobSummaryRepository = new FakeJobSummaryRepository(_outputPath);
            var buildReportRepository = new StubBuildReportRepository();
            var buildReport = buildReportRepository.GetBuildReport();
            var titleWriter = new IncludedModulesWriter(jobSummaryRepository, buildReportRepository);

            // Act
            titleWriter.Write(buildReport);

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expected = Helper.GetExpectedResult("ExpectedIncludedModules.md");
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
