// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    public abstract class AbstractLastBuildReportRepository : IDisposable
    {
        [CanBeNull]
        public abstract UnityEditor.Build.Reporting.BuildReport GetBuildReport();

        public abstract IEnumerable<string> GetReasonsForIncluding(string entityName);

        public abstract void Dispose();
    }
}
