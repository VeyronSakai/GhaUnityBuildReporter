// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using GhaUnityBuildReporter.Editor.Domains;
using UnityEditor;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal class ReportConfigRepository : AbstractReportConfigRepository
    {
        internal override ReportConfig GetReporterConfig(string configPath)
        {
            var configAsset = AssetDatabase.LoadAssetAtPath<GhaUnityBuildReporterConfigAsset>(configPath);
            return new ReportConfig(configAsset.basicInfo);
        }
    }
}
