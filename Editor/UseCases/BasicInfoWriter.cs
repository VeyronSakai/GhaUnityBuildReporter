// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class BasicInfoWriter
    {
        private readonly AbstractJobSummaryRepository _jobSummaryRepository;

        internal BasicInfoWriter([NotNull] AbstractJobSummaryRepository jobSummaryRepository)
        {
            _jobSummaryRepository = jobSummaryRepository;
        }

        internal void Write([NotNull] BuildReport buildReport)
        {
            _jobSummaryRepository.AppendText($"## Basic Info{Environment.NewLine}");

            var summary = buildReport.Summary;

            var basicInfo =
                $"| Key | Value |{Environment.NewLine}"
                + $"| --- | --- |{Environment.NewLine}"
                + $"| Platform | {summary.platform} |{Environment.NewLine}"
                + $@"| Total Time | {summary.totalTime:hh\:mm\:ss\.fff}|{Environment.NewLine}"
                + $"| Total Size | {SizeFormatter.GetFormattedSize(summary.totalSize)} |{Environment.NewLine}"
                + $"| Build Result | {summary.result} |{Environment.NewLine}"
                + $"| Total Errors | {summary.totalErrors} |{Environment.NewLine}"
                + $"| Total Warnings | {summary.totalWarnings} |{Environment.NewLine}";

            _jobSummaryRepository.AppendText(basicInfo);
        }
    }
}
