// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using GhaUnityBuildReporter.Infrastructures;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Presentations
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
