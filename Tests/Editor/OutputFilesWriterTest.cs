// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;
using GhaUnityBuildReporter.Editor.Tests.TestDoubles;
using GhaUnityBuildReporter.Editor.UseCases;
using NUnit.Framework;

namespace GhaUnityBuildReporter.Editor.Tests
{
    public sealed class OutputFilesWriterTest
    {
        private readonly string _outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ActualOutputFiles.md");

        [Test]
        public void WriteTest()
        {
            // Arrange
            var jobSummaryRepository = new FakeJobSummaryRepository(_outputPath);
            var writer = new OutputFilesWriter(jobSummaryRepository);
            var buildReport = Helper.GenerateStubBuildReport();

            // Act
            writer.Write(buildReport);

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expectedResultPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName,
                "Tests",
                "Editor",
                "TestData",
                "ExpectedOutputFiles.md"
            );
            var expected = File.ReadAllText(expectedResultPath);
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
