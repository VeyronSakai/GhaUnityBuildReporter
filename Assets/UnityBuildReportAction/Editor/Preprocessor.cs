// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UnityBuildReportAction.Editor
{
    internal sealed class Preprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (!EnvironmentVariableRepository.IsGitHubActions())
            {
                return;
            }

            EditorQuitEntryPoint.ExecutesUnityBuild = true;
        }
    }
}
