// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.IO;
using GhaUnityBuildReporter.Editor.Domains;

namespace GhaUnityBuildReporter.Editor.Tests.TestDoubles
{
    internal sealed class SpyJobSummaryRepository : AbstractJobSummaryRepository
    {
        private readonly string _outputPath;

        internal SpyJobSummaryRepository([NotNull] string outputPath)
        {
            _outputPath = outputPath;
        }

        internal override void AppendText(string text)
        {
            File.AppendAllText(_outputPath, text);
        }
    }
}
