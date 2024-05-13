// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class PackedAssetInfo
    {
        internal ulong PackedSize { get; }
        internal string SourceAssetPath { get; }

        internal PackedAssetInfo(ulong packedSize, string sourceAssetPath)
        {
            this.PackedSize = packedSize;
            this.SourceAssetPath = sourceAssetPath;
        }
    }
}
