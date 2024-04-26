// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.IO;
using UnityEngine;

namespace UnityBuildReportAction.Editor
{
    internal sealed class EnvironmentVariableRepository
    {
        internal static string GetGitHubStepSummaryPath()
        {
            Debug.Log($"GITHUB_STEP_SUMMARY: {Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY")}");
            var githubStepSummaryPath = Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY");
            if (string.IsNullOrEmpty(githubStepSummaryPath))
            {
                return string.Empty;
            }

            var directoryPath = Directory.GetParent(githubStepSummaryPath)?.FullName;
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return githubStepSummaryPath;
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
