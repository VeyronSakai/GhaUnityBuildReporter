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
            if (configAsset)
            {
                return new ReportConfig(configAsset.title, configAsset.basicInfo, configAsset.buildSteps,
                    configAsset.sourceAssets, configAsset.outputFiles, configAsset.includedModules);
            }

            return new ReportConfig(
                ProjectSettingSingleton.instance.title,
                ProjectSettingSingleton.instance.basicInfo,
                ProjectSettingSingleton.instance.buildSteps,
                ProjectSettingSingleton.instance.sourceAssets,
                ProjectSettingSingleton.instance.outputFiles,
                ProjectSettingSingleton.instance.includedModules
            );
        }
    }
}
