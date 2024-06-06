// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class PackedAssetInfo
    {
        [SerializeField] private ulong packedSize;
        [SerializeField] private string sourceAssetPath;

        internal ulong PackedSize => packedSize;
        internal string SourceAssetPath => sourceAssetPath;

        internal PackedAssetInfo(ulong packedSize, string sourceAssetPath)
        {
            this.packedSize = packedSize;
            this.sourceAssetPath = sourceAssetPath;
        }
    }
}
