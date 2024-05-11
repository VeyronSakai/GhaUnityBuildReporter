// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;

namespace GhaUnityBuildReporter.Editor.Domains
{
    public sealed class StrippingInfo
    {
        public IEnumerable<string> IncludedModules { get; }

        public StrippingInfo(IEnumerable<string> includedModules)
        {
            IncludedModules = includedModules;
        }
    }
}
