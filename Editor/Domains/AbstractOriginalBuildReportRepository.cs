// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal abstract class AbstractOriginalBuildReportRepository : IDisposable
    {
        [CanBeNull]
        internal abstract UnityEditor.Build.Reporting.BuildReport GetBuildReport();

        internal abstract IEnumerable<string> GetReasonsForIncluding(string entityName);

        public abstract void Dispose();
    }
}
