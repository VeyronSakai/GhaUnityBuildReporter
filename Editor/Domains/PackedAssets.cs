// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class PackedAssets
    {
        [NotNull] internal string ShortPath { get; }
        [NotNull] internal IEnumerable<PackedAssetInfo> Contents { get; }

        internal PackedAssets([NotNull] string shortPath, [NotNull] IEnumerable<PackedAssetInfo> contents)
        {
            ShortPath = shortPath;
            Contents = contents;
        }
    }
}
