// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class PackedAssets
    {
        internal PackedAssets(string shortPath, PackedAssetInfo[] contents)
        {
            ShortPath = shortPath;
            Contents = contents;
        }

        internal string ShortPath { get; }
        internal PackedAssetInfo[] Contents { get; }
    }
}
