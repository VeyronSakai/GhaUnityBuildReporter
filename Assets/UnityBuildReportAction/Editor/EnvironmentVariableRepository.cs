// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using UnityEngine;

namespace UnityBuildReportAction.Editor
{
    internal sealed class EnvironmentVariableRepository
    {
        internal static string GetGitHubStepSummaryPath()
        {
            Debug.Log($"GITHUB_STEP_SUMMARY: {Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY")}");
            return Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY");
        }

        internal static bool IsGitHubActions()
        {
            Debug.Log($"GITHUB_ACTIONS: {Environment.GetEnvironmentVariable("GITHUB_ACTIONS")}");
            return Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";
        }

        internal static bool IsOptOut()
        {
            var envVar = Environment.GetEnvironmentVariable("UNITY_BUILD_REPORTER_OPTOUT");
            Debug.Log($"UNITY_BUILD_REPORTER_OPTOUT: {envVar}");
            return envVar == "true" || envVar == "1";
        }
    }
}
