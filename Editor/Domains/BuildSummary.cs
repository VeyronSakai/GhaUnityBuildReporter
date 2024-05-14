// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class BuildSummary
    {
        internal BuildTarget Platform { get; }
        internal TimeSpan TotalTime { get; }
        internal ulong TotalSize { get; }
        internal BuildResult Result { get; }
        internal int TotalErrors { get; }
        internal int TotalWarnings { get; }

        internal BuildSummary(BuildTarget platform, TimeSpan totalTime, ulong totalSize, BuildResult result,
            int totalErrors, int totalWarnings)
        {
            Platform = platform;
            TotalTime = totalTime;
            TotalSize = totalSize;
            Result = result;
            TotalErrors = totalErrors;
            TotalWarnings = totalWarnings;
        }
    }
}
