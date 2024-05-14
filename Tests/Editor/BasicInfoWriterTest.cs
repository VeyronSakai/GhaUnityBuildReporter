// // Copyright (c) 2024 VeyronSakai.
// // This software is released under the MIT License.
//
// using System.IO;
// using System.Linq;
// using GhaUnityBuildReporter.Editor.Tests.TestDoubles;
// using GhaUnityBuildReporter.Editor.UseCases;
// using NUnit.Framework;
// using UnityEditor;
// using UnityEditor.Build.Reporting;
// using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;
// using BuildStep = GhaUnityBuildReporter.Editor.Domains.BuildStep;
// using BuildStepMessage = GhaUnityBuildReporter.Editor.Domains.BuildStepMessage;
// using BuildSummary = GhaUnityBuildReporter.Editor.Domains.BuildSummary;
//
// namespace GhaUnityBuildReporter.Editor.Tests
// {
//     public sealed class BasicInfoWriterTest
//     {
//         private readonly string _outputPath =
//             Path.Combine(Directory.GetCurrentDirectory(), "BasicInfoWriterTestResult.md");
//
//         [Test]
//         public void WriteTest()
//         {
//             // Arrange
//             var jobSummaryRepository = new SpyJobSummaryRepository(_outputPath);
//             var writer = new BasicInfoWriter(jobSummaryRepository);
//             var buildReport       = GenerateMockBuildReport();
//
//             // Act
//             writer.Write(buildReport);
//
//             // Assert
//         }
//
//         private BuildReport GenerateMockBuildReport()
//         {
//             var summary = new BuildSummary(
//                 BuildTarget.Android,
//                 new System.TimeSpan(0, 1, 2, 3, 4),
//                 100,
//                 BuildResult.Succeeded,
//                 1,
//                 2
//             );
//
//             var buildStep1 = new BuildStep("Build Player", new System.TimeSpan(0, 0, 1, 2, 3),
//                 Enumerable.Empty<BuildStepMessage>(), 0);
//         }
//     }
// }


