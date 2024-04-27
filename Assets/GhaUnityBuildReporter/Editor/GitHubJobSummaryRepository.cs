// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;

namespace GhaUnityBuildReporter.Editor
{
    internal sealed class GitHubJobSummaryRepository : IJobSummaryRepository
    {
        private readonly string _gitHubStepSummaryPath;

        internal GitHubJobSummaryRepository(string gitHubStepSummaryPath)
        {
            _gitHubStepSummaryPath = gitHubStepSummaryPath;
        }

        public void AppendText(string text)
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

            File.AppendAllText(_gitHubStepSummaryPath, text);
        }
    }
}
