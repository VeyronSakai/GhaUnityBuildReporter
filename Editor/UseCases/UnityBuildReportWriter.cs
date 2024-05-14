// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal sealed class UnityBuildReportWriter
    {
        [NotNull] private readonly AbstractJobSummaryRepository _jobSummaryRepository;
        [NotNull] private readonly AbstractOriginalBuildReportRepository _originalBuildReportRepository;
        [NotNull] private readonly AbstractBuildReportFactory _buildReportFactory;
        [NotNull] private readonly TitleWriter _titleWriter;
        [NotNull] private readonly BasicInfoWriter _basicInfoWriter;
        [NotNull] private readonly BuildStepsWriter _buildStepsWriter;
        [NotNull] private readonly SourceAssetsWriter _sourceAssetsWriter;
        [NotNull] private readonly OutputFilesWriter _outputFilesWriter;
        [NotNull] private readonly IncludedModulesWriter _includedModulesWriter;

        internal UnityBuildReportWriter(
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
            _outputFilesWriter = new OutputFilesWriter(_jobSummaryRepository);
            _includedModulesWriter = new IncludedModulesWriter(_jobSummaryRepository, _originalBuildReportRepository);
        }

        internal void WriteAll()
        {
            var originalBuildReport = _originalBuildReportRepository.GetBuildReport();
            if (originalBuildReport == null)
            {
                _jobSummaryRepository.AppendText("Unity build report not found.");
                return;
            }

            var buildReport = _buildReportFactory.CreateBuildReport(originalBuildReport);

            _titleWriter.Write();
            _basicInfoWriter.Write(buildReport);
            _buildStepsWriter.Write(buildReport);
            _sourceAssetsWriter.Write(buildReport);
            _outputFilesWriter.Write(buildReport);
            _includedModulesWriter.Write(buildReport);
        }
    }
}
