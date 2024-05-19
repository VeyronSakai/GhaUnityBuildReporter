// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using GhaUnityBuildReporter.Editor.Domains;
using UnityEditor;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal class ReportConfigRepository : AbstractReportConfigRepository
    {
        private readonly string _configPath;

        internal ReportConfigRepository(string configPath)
        {
            _configPath = configPath;
        }

        internal override ReportConfig GetReporterConfig()
        {
            var configAsset = AssetDatabase.LoadAssetAtPath<GhaUnityBuildReporterConfigAsset>(_configPath);
            return !configAsset
                ? null
                : new ReportConfig(configAsset.title, configAsset.basicInfo, configAsset.buildSteps,
                    configAsset.sourceAssets, configAsset.outputFiles, configAsset.includedModules);
        }
    }
}
