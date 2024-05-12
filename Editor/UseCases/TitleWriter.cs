// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using GhaUnityBuildReporter.Editor.Domains;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class TitleWriter
    {
        private readonly AbstractJobSummaryRepository _jobSummaryRepository;

        internal TitleWriter(AbstractJobSummaryRepository jobSummaryRepository)
        {
            _jobSummaryRepository = jobSummaryRepository;
        }

        internal void WriteTitle(BuildReport buildReport)
        {
            _jobSummaryRepository.AppendText($"# Unity Build Report{Environment.NewLine}");
            if (buildReport == null)
            {
                _jobSummaryRepository.AppendText($"Build report not found {Environment.NewLine}");
            }
        }
    }
}
