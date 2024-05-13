// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class StrippingInfo
    {
        [NotNull] internal IEnumerable<string> IncludedModules { get; }

        internal StrippingInfo([NotNull] IEnumerable<string> includedModules)
        {
            IncludedModules = includedModules;
        }
    }
}
