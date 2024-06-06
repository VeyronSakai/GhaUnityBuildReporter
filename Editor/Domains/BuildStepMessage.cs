// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class BuildStepMessage
    {
        [SerializeField] private LogType type;
        [SerializeField] private string content;

        internal LogType Type => type;
        internal string Content => content;

        internal BuildStepMessage(LogType type, string content)
        {
            this.type = type;
            this.content = content;
        }
    }
}
