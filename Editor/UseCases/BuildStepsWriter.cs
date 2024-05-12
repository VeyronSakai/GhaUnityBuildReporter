// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class BuildStepsWriter
    {
        [NotNull] private readonly AbstractJobSummaryRepository _jobSummaryRepository;

        internal BuildStepsWriter([NotNull] AbstractJobSummaryRepository jobSummaryRepository)
        {
            _jobSummaryRepository = jobSummaryRepository;
        }

        internal void Write([NotNull] BuildReport buildReport)
        {
            if (buildReport.Steps.Length <= 0)
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Build Steps{Environment.NewLine}" +
                                             $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}");

            foreach (var step in buildReport.Steps)
            {
                switch (step.depth)
                {
                    case 0:
                        _jobSummaryRepository.AppendText(
                            $@"### {step.name} ({step.duration:hh\:mm\:ss\.fff}){Environment.NewLine}");
                        break;
                    case >= 1:
                        {
                            _jobSummaryRepository.AppendText(
                                $@"{new string(' ', (step.depth - 1) * 2)}- **{step.name}** ({step.duration:hh\:mm\:ss\.fff}){Environment.NewLine}");

                            if (step.messages.Length <= 0)
                            {
                                continue;
                            }

                            foreach (var message in step.messages)
                            {
                                var emoji = message.type switch
                                {
                                    LogType.Error => ":x:",
                                    LogType.Assert => ":no_entry_sign:",
                                    LogType.Warning => ":warning:",
                                    LogType.Log => ":information_source:",
                                    LogType.Exception => ":boom:",
                                    _ => ":question:"
                                };

                                _jobSummaryRepository.AppendText(
                                    $@"{new string(' ', step.depth * 2)}- {emoji} {message.content}{Environment.NewLine}");
                            }

                            break;
                        }
                }
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }
    }
}
