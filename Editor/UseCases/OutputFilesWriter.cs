// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.IO;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class OutputFilesWriter
    {
        [NotNull] private readonly AbstractJobSummaryRepository _jobSummaryRepository;

        internal OutputFilesWriter([NotNull] AbstractJobSummaryRepository jobSummaryRepository)
        {
            _jobSummaryRepository = jobSummaryRepository;
        }

        internal void Write([NotNull] BuildReport buildReport)
        {
            if (buildReport.BuildFiles.Length == 0)
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Output Files{Environment.NewLine}" +
                                             $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}" +
                                             $"| File | Size |{Environment.NewLine}" +
                                             $"| --- | --- |{Environment.NewLine}");

            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;

            foreach (var file in buildReport.BuildFiles)
            {
                var relativePath = Path.GetRelativePath(projectRootPath, file.Path);
                _jobSummaryRepository.AppendText(
                    $"| {relativePath} | {SizeFormatter.GetFormattedSize(file.Size)} |{Environment.NewLine}");
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }
    }
}
