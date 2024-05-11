// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Editor.Domains
{
    public sealed class PackedAssets
    {
        public string ShortPath { get; }
        public PackedAssetInfo[] Contents { get; }

        public PackedAssets(string shortPath, PackedAssetInfo[] contents)
        {
            ShortPath = shortPath;
            Contents = contents;
        }
    }
}
