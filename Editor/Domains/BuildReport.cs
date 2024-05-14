// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class BuildReport
    {
        [NotNull] internal BuildSummary Summary { get; }
        [NotNull] internal BuildStep[] Steps { get; }
        [NotNull] internal PackedAssets[] PackedAssets { get; }
        [NotNull] internal StrippingInfo StrippingInfo { get; }
        [NotNull] internal BuildFile[] BuildFiles { get; }

        internal BuildReport(
            [NotNull] BuildSummary summary,
            [NotNull] BuildStep[] steps,
            [NotNull] PackedAssets[] packedAssets,
            [NotNull] StrippingInfo strippingInfo,
            [NotNull] BuildFile[] buildFiles
        )
        {
            Summary = summary;
            Steps = steps;
            PackedAssets = packedAssets;
            StrippingInfo = strippingInfo;
            BuildFiles = buildFiles;
        }
    }
}
