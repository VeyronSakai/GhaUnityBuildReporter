// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    public interface ILastBuildReportRepository : IDisposable
    {
        [CanBeNull]
        UnityEditor.Build.Reporting.BuildReport GetBuildReport();

        IEnumerable<string> GetReasonsForIncluding(string entityName);
    }
}
