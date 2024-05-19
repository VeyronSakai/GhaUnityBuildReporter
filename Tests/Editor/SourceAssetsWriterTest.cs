// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Diagnostics;
using System.IO;
using GhaUnityBuildReporter.Editor.Tests.TestDoubles;
using GhaUnityBuildReporter.Editor.UseCases;
using NUnit.Framework;

namespace GhaUnityBuildReporter.Editor.Tests
{
    public sealed class SourceAssetsWriterTest
    {
        private readonly string _outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ActualSourceAssets.md");

        [Test]
        public void WriteTest()
        {
            // Arrange
            var jobSummaryRepository = new FakeJobSummaryRepository(_outputPath);
            var buildReport = Helper.GenerateStubBuildReport();
            var writer = new SourceAssetsWriter(jobSummaryRepository);

            // Act
            Debug.Assert(buildReport != null, nameof(buildReport) + " != null");
            writer.Write(buildReport);

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expected = Helper.GetExpectedResult("ExpectedSourceAssets.md");
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
