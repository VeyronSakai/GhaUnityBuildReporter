// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class UnityBuildReportWriter
    {
        [NotNull] private readonly AbstractReportConfigRepository _reportConfigRepository;
        [NotNull] private readonly BuildReport _buildReport;
        [NotNull] private readonly TitleWriter _titleWriter;
        [NotNull] private readonly BasicInfoWriter _basicInfoWriter;
        [NotNull] private readonly BuildStepsWriter _buildStepsWriter;
        [NotNull] private readonly SourceAssetsWriter _sourceAssetsWriter;
        [NotNull] private readonly OutputFilesWriter _outputFilesWriter;
        [NotNull] private readonly IncludedModulesWriter _includedModulesWriter;

        internal UnityBuildReportWriter(
            [NotNull] AbstractJobSummaryRepository jobSummaryRepository,
            [NotNull] AbstractBuildReportRepository buildReportRepository,
            [NotNull] AbstractReportConfigRepository reportConfigRepository
        )
        {
            _reportConfigRepository = reportConfigRepository;
            _buildReport = buildReportRepository.GetBuildReport() ??
                           throw new InvalidOperationException("Build report is not available.");
            _titleWriter = new TitleWriter(jobSummaryRepository);
            _basicInfoWriter = new BasicInfoWriter(jobSummaryRepository);
            _buildStepsWriter = new BuildStepsWriter(jobSummaryRepository);
            _sourceAssetsWriter = new SourceAssetsWriter(jobSummaryRepository);
            _outputFilesWriter = new OutputFilesWriter(jobSummaryRepository);
            _includedModulesWriter = new IncludedModulesWriter(jobSummaryRepository, buildReportRepository);
        }

        internal void Write()
        {
            var reportConfig = _reportConfigRepository.GetReporterConfig();

            if (reportConfig.WritesTitle)
            {
                _titleWriter.Write();
            }

            if (reportConfig.WritesBasicInfo)
            {
                _basicInfoWriter.Write(_buildReport);
            }

            if (reportConfig.WritesBuildSteps)
            {
                _buildStepsWriter.Write(_buildReport);
            }

            if (reportConfig.WritesSourceAssets)
            {
                _sourceAssetsWriter.Write(_buildReport);
            }

            if (reportConfig.WritesOutputFiles)
            {
                _outputFilesWriter.Write(_buildReport);
            }

            if (reportConfig.WritesIncludedModules)
            {
                _includedModulesWriter.Write(_buildReport);
            }
        }
    }
}
