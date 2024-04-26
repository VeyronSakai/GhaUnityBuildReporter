// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System.IO;

namespace UnityBuildReportAction.Editor
{
    internal sealed class GitHubJobSummaryRepository : IJobSummaryRepository
    {
        private readonly string _gitHubStepSummaryPath;

        internal GitHubJobSummaryRepository(string gitHubStepSummaryPath)
        {
            _gitHubStepSummaryPath = gitHubStepSummaryPath;
        }

        void IJobSummaryRepository.AppendText(string text)
        {
            File.AppendAllText(_gitHubStepSummaryPath, text);
        }
    }
}
