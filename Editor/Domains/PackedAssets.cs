// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Domains
{
    public sealed class PackedAssets
    {
        public PackedAssets(string shortPath, PackedAssetInfo[] contents)
        {
            ShortPath = shortPath;
            Contents = contents;
        }

        public string ShortPath { get; }
        public PackedAssetInfo[] Contents { get; }
    }
}
