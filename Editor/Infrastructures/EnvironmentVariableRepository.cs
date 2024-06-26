// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal sealed class EnvironmentVariableRepository
    {
        private const string GitHubStepSummary = "GITHUB_STEP_SUMMARY";
        private const string GitHubActions = "GITHUB_ACTIONS";
        private const string GhaUnityBuildReporterOptOut = "GHA_UNITY_BUILD_REPORTER_OPTOUT";
        private const string True = "true";

        [CanBeNull]
        internal static string GetGitHubStepSummaryPath()
        {
            return Environment.GetEnvironmentVariable(GitHubStepSummary);
        }

        internal static bool IsGitHubActions()
        {
            return Environment.GetEnvironmentVariable(GitHubActions) == True;
        }

        internal static bool IsDisabled()
        {
            var envVar = Environment.GetEnvironmentVariable(GhaUnityBuildReporterOptOut);
            return envVar?.ToLower() is True or "1";
        }
    }
}
