// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using JetBrains.Annotations;

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal abstract class AbstractBuildReportFactory
    {
        [NotNull]
        internal abstract BuildReport CreateBuildReport(
            [NotNull] UnityEditor.Build.Reporting.BuildReport originalBuildReport);
    }
}
