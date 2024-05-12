// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;
using GhaUnityBuildReporter.Editor.Tests.TestDoubles;
using GhaUnityBuildReporter.Editor.UseCases;
using NUnit.Framework;

namespace GhaUnityBuildReporter.Editor.Tests
{
    public class TitleWriterTest
    {
        private readonly string _outputPath =
            Path.Combine(Directory.GetCurrentDirectory(), "ActualTitleWriterTestResult.md");

        [Test]
        public void WriteTest()
        {
            // Arrange
            var jobSummaryRepository = new SpyJobSummaryRepository(_outputPath);
            var titleWriter = new TitleWriter(jobSummaryRepository);

            // Act
            titleWriter.Write();

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expectedResultPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName,
                "Tests",
                "Editor",
                "TestData",
                "ExpectedTitleWriterTestResult.md"
            );
            var expected = File.ReadAllText(expectedResultPath);
            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_outputPath);
        }
    }
}
