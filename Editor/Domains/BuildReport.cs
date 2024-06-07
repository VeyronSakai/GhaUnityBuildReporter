// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class BuildReport
    {
        [SerializeField] private BuildSummary summary;
        [SerializeField] private BuildStep[] steps;
        [SerializeField] private PackedAssets[] packedAssets;
        [SerializeField] private StrippingInfo strippingInfo;
        [SerializeField] private BuildFile[] buildFiles;

        [NotNull] internal BuildSummary Summary => summary;
        [NotNull] internal BuildStep[] Steps => steps;
        [NotNull] internal PackedAssets[] PackedAssets => packedAssets;
        [NotNull] internal StrippingInfo StrippingInfo => strippingInfo;
        [NotNull] internal BuildFile[] BuildFiles => buildFiles;

        internal BuildReport(
            [NotNull] BuildSummary summary,
            [NotNull] BuildStep[] steps,
            [NotNull] PackedAssets[] packedAssets,
            [NotNull] StrippingInfo strippingInfo,
            [NotNull] BuildFile[] buildFiles
        )
        {
            this.summary = summary;
            this.steps = steps;
            this.packedAssets = packedAssets;
            this.strippingInfo = strippingInfo;
            this.buildFiles = buildFiles;
        }
    }
}
