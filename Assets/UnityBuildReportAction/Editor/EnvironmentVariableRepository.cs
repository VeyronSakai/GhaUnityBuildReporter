// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System;

namespace UnityBuildReportAction.Editor
{
    internal sealed class EnvironmentVariableRepository
    {
        internal static string GetGitHubStepSummaryPath()
        {
            return Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY");
        }

        internal static bool IsGitHubActions()
        {
            return Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";
        }

        internal static bool IsOptOut()
        {
            var envVar = Environment.GetEnvironmentVariable("UNITY_BUILD_REPORTER_OPTOUT");
            return envVar == "true" || envVar == "1";
        }
    }
}
