// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal sealed class GitHubJobSummaryRepository : AbstractJobSummaryRepository
    {
        private readonly string _gitHubStepSummaryPath;

        internal GitHubJobSummaryRepository([NotNull] string gitHubStepSummaryPath)
        {
            _gitHubStepSummaryPath = gitHubStepSummaryPath;
        }

        internal override void AppendText(string text)
        {
            var dir = Path.GetDirectoryName(_gitHubStepSummaryPath);
            if (string.IsNullOrEmpty(dir))
            {
                return;
            }

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // UnityEditor cannot wait for asynchronous processing when exiting, use synchronous version instead
            File.AppendAllText(_gitHubStepSummaryPath, text);
        }
    }
}
