// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;

namespace GhaUnityBuildReporter.Domains
{
    public sealed class StrippingInfo
    {
        public StrippingInfo(IEnumerable<string> includedModules)
        {
            IncludedModules = includedModules;
        }

        public IEnumerable<string> IncludedModules { get; }
    }
}
