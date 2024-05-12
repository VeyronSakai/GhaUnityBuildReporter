// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Editor.Domains
{
    public sealed class BuildReport
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

        public BuildSummary Summary { get; }
        public BuildStep[] Steps { get; }
        public PackedAssets[] PackedAssets { get; }
        public StrippingInfo StrippingInfo { get; }
        public BuildFile[] BuildFiles { get; }
    }
}
