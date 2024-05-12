// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class TitleWriter
    {
        [NotNull] private readonly AbstractJobSummaryRepository _jobSummaryRepository;

        internal TitleWriter([NotNull] AbstractJobSummaryRepository jobSummaryRepository)
        {
            _jobSummaryRepository = jobSummaryRepository;
        }

        internal void Write()
        {
            _jobSummaryRepository.AppendText($"# Unity Build Report{Environment.NewLine}");
        }
    }
}
