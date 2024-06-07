// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    public abstract class AbstractBuildReportRepository : IDisposable
    {
        [CanBeNull]
        internal abstract BuildReport GetBuildReport();

        [NotNull]
        internal abstract IEnumerable<string> GetReasonsForIncluding(string entityName);

        internal abstract void WriteJson();

        public abstract void Dispose();
    }
}
