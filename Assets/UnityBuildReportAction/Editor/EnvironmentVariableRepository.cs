// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using UnityEngine;

namespace UnityBuildReportAction.Editor
{
    internal sealed class EnvironmentVariableRepository
    {
        private const string GitHubStepSummary = "GITHUB_STEP_SUMMARY";
        private const string GitHubActions = "GITHUB_ACTIONS";
        private const string GhaUnityBuildReporterOptOut = "GHA_UNITY_BUILD_REPORTER_OPTOUT";
        private const string True = "true";

        internal static string GetGitHubStepSummaryPath()
        {
            Debug.Log($"{GitHubStepSummary}: {Environment.GetEnvironmentVariable(GitHubStepSummary)}");
            return Environment.GetEnvironmentVariable(GitHubStepSummary);
        }

        internal static bool IsGitHubActions()
        {
            Debug.Log($"{GitHubActions}: {Environment.GetEnvironmentVariable(GitHubActions)}");
            return Environment.GetEnvironmentVariable(GitHubActions) == True;
        }

        internal static bool IsDisabled()
        {
            var envVar = Environment.GetEnvironmentVariable(GhaUnityBuildReporterOptOut);
            Debug.Log($"{GhaUnityBuildReporterOptOut}: {envVar}");
            return envVar?.ToLower() is True or "1";
        }
    }
}
