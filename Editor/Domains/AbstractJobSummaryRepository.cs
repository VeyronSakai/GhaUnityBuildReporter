// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal abstract class AbstractJobSummaryRepository
    {
        internal abstract void AppendText([NotNull] string text);
    }
}
