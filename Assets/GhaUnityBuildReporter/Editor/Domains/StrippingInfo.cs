// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.Linq;

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
