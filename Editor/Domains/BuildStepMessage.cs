// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class BuildStepMessage
    {
        internal LogType Type { get; }
        internal string Content { get; }

        internal BuildStepMessage(LogType type, string content)
        {
            Type = type;
            Content = content;
        }
    }
}
