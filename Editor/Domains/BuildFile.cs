// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class BuildFile
    {
        [SerializeField] private uint id;
        [SerializeField] private string path;
        [SerializeField] private string role;
        [SerializeField] private ulong size;

        internal uint ID => id;
        internal string Path => path;
        internal string Role => role;
        internal ulong Size => size;

        internal BuildFile(uint id, string path, string role, ulong size)
        {
            this.id = id;
            this.path = path;
            this.role = role;
            this.size = size;
        }
    }
}
