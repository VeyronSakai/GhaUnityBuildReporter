// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class BuildReport
    {
        public BuildReport(BuildSummary summary, BuildStep[] steps, PackedAssets[] packedAssets,
            StrippingInfo strippingInfo, BuildFile[] buildFiles)
        {
            Summary = summary;
            Steps = steps;
            PackedAssets = packedAssets;
            StrippingInfo = strippingInfo;
            BuildFiles = buildFiles;
        }

        internal BuildSummary Summary { get; }
        internal BuildStep[] Steps { get; }
        internal PackedAssets[] PackedAssets { get; }
        internal StrippingInfo StrippingInfo { get; }
        internal BuildFile[] BuildFiles { get; }
    }
}
