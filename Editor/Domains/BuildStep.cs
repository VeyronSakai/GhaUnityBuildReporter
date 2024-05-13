// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class BuildStep
    {
        internal string Name { get; }
        internal TimeSpan Duration { get; }
        [NotNull] internal IEnumerable<BuildStepMessage> Messages { get; }
        internal int Depth { get; }

        internal BuildStep(string name, TimeSpan duration, [NotNull] IEnumerable<BuildStepMessage> messages, int depth)
        {
            this.Name = name;
            Duration = duration;
            Messages = messages;
            Depth = depth;
        }
    }
}
