// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class BuildFile
    {
        internal uint ID { get; }
        internal string Path { get; }
        internal string Role { get; }
        internal ulong Size { get; }

        internal BuildFile(uint id, string path, string role, ulong size)
        {
            ID = id;
            Path = path;
            Role = role;
            Size = size;
        }
    }
}
