// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using JetBrains.Annotations;
using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Editor.Domains
{
    public interface IBuildReportRepository : IDisposable
    {
        [CanBeNull]
        BuildReport GetBuildReport();
    }
}
