// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.IO;
using System.Linq;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;
using UnityEngine;
using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class UnityBuildReporter
    {
        [CanBeNull] private BuildReport _buildReport;
        private readonly AbstractJobSummaryRepository _jobSummaryRepository;
        private readonly AbstractOriginalBuildReportRepository _originalBuildReportRepository;
        private readonly AbstractBuildReportFactory _buildReportFactory;
        private readonly TitleWriter _titleWriter;
        private readonly BasicInfoWriter _basicInfoWriter;
        private readonly BuildStepsWriter _buildStepsWriter;
        private readonly SourceAssetsWriter _sourceAssetsWriter;

        internal UnityBuildReporter(
            [NotNull] AbstractJobSummaryRepository jobSummaryRepository,
            [NotNull] AbstractOriginalBuildReportRepository originalBuildReportRepository,
            [NotNull] AbstractBuildReportFactory buildReportFactory
        )
        {
            _jobSummaryRepository = jobSummaryRepository;
            _originalBuildReportRepository = originalBuildReportRepository;
            _buildReportFactory = buildReportFactory;
            _titleWriter = new TitleWriter(_jobSummaryRepository);
            _basicInfoWriter = new BasicInfoWriter(_jobSummaryRepository);
            _buildStepsWriter = new BuildStepsWriter(_jobSummaryRepository);
            _sourceAssetsWriter = new SourceAssetsWriter(_jobSummaryRepository);
        }

        internal void WriteAll()
        {
            var originalBuildReport = _originalBuildReportRepository.GetBuildReport();
            if (originalBuildReport == null)
            {
                _jobSummaryRepository.AppendText("Unity build report not found.");
                return;
            }

            _buildReport = _buildReportFactory.CreateBuildReport(originalBuildReport);

            _titleWriter.Write();
            _basicInfoWriter.Write(_buildReport);
            _buildStepsWriter.Write(_buildReport);
            _sourceAssetsWriter.Write(_buildReport);

            WriteOutputFilesInfo();
            WriteIncludedModulesInfo();
        }

        private void WriteOutputFilesInfo()
        {
            if (_buildReport == null || _buildReport.BuildFiles.Length == 0)
            {
                return;
            }

            _jobSummaryRepository.AppendText($"## Output Files{Environment.NewLine}" +
                                             $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}" +
                                             $"| File | Size |{Environment.NewLine}" +
                                             $"| --- | --- |{Environment.NewLine}");

            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;

            foreach (var file in _buildReport.BuildFiles)
            {
                var relativePath = Path.GetRelativePath(projectRootPath, file.path);
                _jobSummaryRepository.AppendText(
                    $"| {relativePath} | {GetFormattedSize(file.size)} |{Environment.NewLine}");
            }

            _jobSummaryRepository.AppendText($"</details>{Environment.NewLine}{Environment.NewLine}");
        }

        private void WriteIncludedModulesInfo()
        {
            if (_buildReport?.StrippingInfo == null || !_buildReport.StrippingInfo.IncludedModules.Any())
            {
                return;
            }

            _jobSummaryRepository.AppendText(
                $"## Included Modules{Environment.NewLine}" +
                $"<details><summary>Details</summary>{Environment.NewLine}{Environment.NewLine}"
            );

            foreach (var item in _buildReport.StrippingInfo.IncludedModules)
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

            if (_buildReport == null)
            {
                return;
            }

            var reasons = _originalBuildReportRepository.GetReasonsForIncluding(item);
            foreach (var reason in reasons)
            {
                WriteIncludedModuleInfoInternal(reason, depth + 1);
            }
        }

        private static string GetFormattedSize(ulong size)
        {
            return size switch
            {
                < 1024 => size + " B",
                < 1024 * 1024 => (size / 1024.00).ToString("F2") + " KB",
                < 1024 * 1024 * 1024 => (size / (1024.0 * 1024.0)).ToString("F2") + " MB",
                _ => (size / (1024.0 * 1024.0 * 1024.0)).ToString("F2") + " GB"
            };
        }
    }
}
