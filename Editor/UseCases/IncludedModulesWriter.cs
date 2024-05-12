// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GhaUnityBuildReporter.Editor.Domains;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class IncludedModulesWriter
    {
        [NotNull] private readonly AbstractJobSummaryRepository _jobSummaryRepository;
        [NotNull] private readonly AbstractOriginalBuildReportRepository _originalBuildReportRepository;

        internal IncludedModulesWriter([NotNull] AbstractJobSummaryRepository jobSummaryRepository,
            [NotNull] AbstractOriginalBuildReportRepository originalBuildReportRepository)
        {
            _jobSummaryRepository = jobSummaryRepository;
            _originalBuildReportRepository = originalBuildReportRepository;
        }

        internal void Write([NotNull] BuildReport buildReport)
        {
            if (!buildReport.StrippingInfo.IncludedModules.Any())
            {
                return;
            }

            _jobSummaryRepository.AppendText(
                $"## Included Modules{Environment.NewLine}" +
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}"
            );

            foreach (var item in buildReport.StrippingInfo.IncludedModules)
            {
                WriteIncludedModuleInfoInternal(item, 0);
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }

        private void WriteIncludedModuleInfoInternal(string item, uint depth)
        {
            _jobSummaryRepository.AppendText(depth == 0
                ? $@"- **{item}**{Environment.NewLine}"
                : $@"{new string(' ', (int)(depth * 2))} - {item}{Environment.NewLine}");

            var reasons = _originalBuildReportRepository.GetReasonsForIncluding(item);
            foreach (var reason in reasons)
            {
                WriteIncludedModuleInfoInternal(reason, depth + 1);
            }
        }
    }
}
