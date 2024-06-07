// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class StrippingInfo
    {
        [SerializeField] private string[] includedModules;

        [NotNull] internal string[] IncludedModules => includedModules;

        internal StrippingInfo([NotNull] IEnumerable<string> includedModules)
        {
            this.includedModules = includedModules.ToArray();
        }
    }
}
