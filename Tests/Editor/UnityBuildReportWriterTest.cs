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
        public void WriteTest()
        {
            // Arrange
            var jobSummaryRepository = new FakeJobSummaryRepository(_outputPath);
            var buildReportRepository = new StubBuildReportRepository();
            var writer = new UnityBuildReportWriter(jobSummaryRepository, buildReportRepository);

            // Act
            writer.Write();

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expected = Helper.GetExpectedResult("ExpectedUnityBuildReport.md");
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
