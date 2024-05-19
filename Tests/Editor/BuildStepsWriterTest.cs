// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;
using GhaUnityBuildReporter.Editor.Tests.TestDoubles;
using GhaUnityBuildReporter.Editor.UseCases;
using NUnit.Framework;

namespace GhaUnityBuildReporter.Editor.Tests
{
    internal sealed class BuildStepsWriterTest
    {
        private readonly string _outputPath =
            Path.Combine(Directory.GetCurrentDirectory(), "ActualBuildSteps.md");

        [Test]
        public void WriteTest()
        {
            // Arrange
            var jobSummaryRepository = new FakeJobSummaryRepository(_outputPath);
            var writer = new BuildStepsWriter(jobSummaryRepository);
            var buildReport = Helper.GenerateStubBuildReport();

            // Act
            writer.Write(buildReport);

            // Assert
            var actual = File.ReadAllText(_outputPath);
            var expected = Helper.GetExpectedResult("ExpectedBuildSteps.md");
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
