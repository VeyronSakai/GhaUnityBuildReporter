// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using JetBrains.Annotations;
using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class PackedAssets
    {
        internal PackedAssets([NotNull] string shortPath, [NotNull] PackedAssetInfo[] contents)
        {
            ShortPath = shortPath;
            Contents = contents;
        }

        [NotNull] internal string ShortPath { get; }
        [NotNull] internal PackedAssetInfo[] Contents { get; }
    }
}
