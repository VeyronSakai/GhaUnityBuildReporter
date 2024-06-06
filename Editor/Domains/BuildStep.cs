// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class BuildStep
    {
        [SerializeField] private string name;

        // ReSharper disable once InconsistentNaming
        private TimeSpan duration;

        internal string Name => name;
        internal TimeSpan Duration => duration;
        [NotNull] internal IEnumerable<BuildStepMessage> Messages { get; }
        internal int Depth { get; }

        internal BuildStep(string name, TimeSpan duration, [NotNull] IEnumerable<BuildStepMessage> messages, int depth)
        {
            this.name = name;
            this.duration = duration;
            Messages = messages;
            Depth = depth;
        }
    }
}
