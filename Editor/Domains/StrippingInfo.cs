// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class StrippingInfo
    {
        internal StrippingInfo(IEnumerable<string> includedModules)
        {
            IncludedModules = includedModules;
        }

        internal IEnumerable<string> IncludedModules { get; }
    }
}
