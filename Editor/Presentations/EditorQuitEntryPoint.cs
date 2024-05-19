// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using GhaUnityBuildReporter.Editor.Infrastructures;
using GhaUnityBuildReporter.Editor.UseCases;
using UnityEditor;

namespace GhaUnityBuildReporter.Editor.Presentations
{
    [InitializeOnLoad]
    internal sealed class EditorQuitEntryPoint
    {
        internal static bool ExecutesUnityBuild;
        private static readonly string s_gitHubStepSummaryPath;

        // ReSharper disable once CommentTypo
        // The 'report' argument passed to IPostprocessBuildWithReport.OnPostprocessBuild() contains incorrect information, so read Library/LastBuild.buildreport instead.
        // see: https://issuetracker.unity3d.com/issues/buildreport-report-in-ipostprocessbuildwithreport-provides-incorrect-information
        static EditorQuitEntryPoint()
        {
            s_gitHubStepSummaryPath = EnvironmentVariableRepository.GetGitHubStepSummaryPath();

            if (!EnvironmentVariableRepository.IsGitHubActions() || string.IsNullOrEmpty(s_gitHubStepSummaryPath) ||
                EnvironmentVariableRepository.IsDisabled())
            {
                return;
            }

            EditorApplication.quitting += Quit;
        }

        private static void Quit()
        {
            if (!ExecutesUnityBuild)
            {
                return;
            }

            using var buildReportRepository = new BuildReportRepository();
            var jobSummaryRepository = new GitHubJobSummaryRepository(s_gitHubStepSummaryPath);

            var buildReport = buildReportRepository.GetBuildReport();
            if (buildReport == null)
            {
                jobSummaryRepository.AppendText("Unity build report not found.");
                return;
            }

            var commandLineArgsRepository = new CommandLineArgsRepository();
            var configPath = commandLineArgsRepository.GetValue("buildReporterConfig");
            var reportConfigRepository = new ReportConfigRepository(configPath);
            var reporter =
                new UnityBuildReportWriter(jobSummaryRepository, buildReportRepository, reportConfigRepository);
            reporter.Write();
        }
    }
}
