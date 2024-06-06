// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class PackedAssets
    {
        [SerializeField] private string shortPath;
        [SerializeField] private PackedAssetInfo[] contents;

        [NotNull] internal string ShortPath => shortPath;
        [NotNull] internal IEnumerable<PackedAssetInfo> Contents => contents;

        internal PackedAssets([NotNull] string shortPath, [NotNull] PackedAssetInfo[] contents)
        {
            this.shortPath = shortPath;
            this.contents = contents;
        }
    }
}
