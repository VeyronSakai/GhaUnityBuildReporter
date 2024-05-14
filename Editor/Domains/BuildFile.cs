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
            this.ID = id;
            this.Path = path;
            this.Role = role;
            this.Size = size;
        }
    }
}
