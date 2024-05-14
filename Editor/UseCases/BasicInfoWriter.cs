// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class BasicInfoWriter
    {
        [NotNull] private readonly AbstractJobSummaryRepository _jobSummaryRepository;

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
                + $"| Platform | {summary.Platform} |{Environment.NewLine}"
                + $@"| Total Time | {summary.TotalTime:hh\:mm\:ss\.fff}|{Environment.NewLine}"
                + $"| Total Size | {SizeFormatter.GetFormattedSize(summary.TotalSize)} |{Environment.NewLine}"
                + $"| Build Result | {summary.Result} |{Environment.NewLine}"
                + $"| Total Errors | {summary.TotalErrors} |{Environment.NewLine}"
                + $"| Total Warnings | {summary.TotalWarnings} |{Environment.NewLine}";

            _jobSummaryRepository.AppendText(basicInfo);
        }
    }
}
