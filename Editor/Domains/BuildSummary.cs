// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class BuildSummary
    {
        [SerializeField] private BuildTarget platform;
        [SerializeField] private string totalTime;
        [SerializeField] private ulong totalSize;
        [SerializeField] private BuildResult result;
        [SerializeField] private int totalErrors;
        [SerializeField] private int totalWarnings;

        internal BuildTarget Platform => platform;
        internal TimeSpan TotalTime { get; }
        internal ulong TotalSize => totalSize;
        internal BuildResult Result => result;
        internal int TotalErrors => totalErrors;
        internal int TotalWarnings => totalWarnings;

        internal BuildSummary(BuildTarget platform, TimeSpan totalTime, ulong totalSize, BuildResult result,
            int totalErrors, int totalWarnings)
        {
            this.platform = platform;
            TotalTime = totalTime;
            this.totalTime = totalTime.ToString();
            this.totalSize = totalSize;
            this.result = result;
            this.totalErrors = totalErrors;
            this.totalWarnings = totalWarnings;
        }
    }
}
