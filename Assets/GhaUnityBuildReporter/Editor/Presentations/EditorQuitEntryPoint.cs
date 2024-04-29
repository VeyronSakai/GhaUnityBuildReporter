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

        // The 'report' argument passed to IPolsstprocessBuildWithReport.OnPostprocessBuild() contains incorrect information, so read Library/LastBuild.buildreport instead.
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
            if (!buildReportRepository.IsBuildReportAvailable())
            {
                return;
            }

            var jobSummaryRepository = new GitHubJobSummaryRepository(s_gitHubStepSummaryPath);
            var useCase = new ReportUnityBuildUseCase(jobSummaryRepository, buildReportRepository);
            useCase.WriteAll();
        }
    }
}