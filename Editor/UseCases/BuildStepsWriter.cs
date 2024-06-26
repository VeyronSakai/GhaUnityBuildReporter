// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Linq;
using System.Text.RegularExpressions;
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
                switch (step.Depth)
                {
                    case 0:
                        _jobSummaryRepository.AppendText(
                            $@"### {step.Name} ({step.Duration:hh\:mm\:ss\.fff}){Environment.NewLine}");
                        break;
                    case >= 1:
                        {
                            _jobSummaryRepository.AppendText(
                                $@"{new string(' ', (step.Depth - 1) * 2)}- **{step.Name}** ({step.Duration:hh\:mm\:ss\.fff}){Environment.NewLine}");

                            if (!step.Messages.Any())
                            {
                                continue;
                            }

                            foreach (var message in step.Messages)
                            {
                                var emoji = message.Type switch
                                {
                                    LogType.Error => ":x:",
                                    LogType.Assert => ":no_entry_sign:",
                                    LogType.Warning => ":warning:",
                                    LogType.Log => ":information_source:",
                                    LogType.Exception => ":boom:",
                                    _ => ":question:"
                                };

                                // If there are multiple consecutive line breaks, combine them into one line break
                                var normalizedMessage = Regex.Replace(message.Content, @"(\r\n|\r|\n)+",
                                    Environment.NewLine);
                                var contents = normalizedMessage.Split(Environment.NewLine);
                                foreach (var content in contents)
                                {
                                    _jobSummaryRepository.AppendText(
                                        $@"{new string(' ', step.Depth * 2)}- {emoji} {content}{Environment.NewLine}"
                                    );
                                }
                            }

                            break;
                        }
                }
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }
    }
}
