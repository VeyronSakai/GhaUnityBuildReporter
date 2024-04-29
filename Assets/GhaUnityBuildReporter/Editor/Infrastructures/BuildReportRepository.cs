// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using GhaUnityBuildReporter.Editor.Domains;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal sealed class BuildReportRepository : IBuildReportRepository, IDisposable
    {
        private readonly string _buildReportDir =
            $"{Path.Combine(Application.dataPath, LastBuildReportsDirectoryName)}";

        private readonly string _lastBuildReportsAssetPath =
            $"{Path.Combine("Assets", LastBuildReportsDirectoryName, LastBuildReportFileName)}";

        private const string LastBuildReportsDirectoryName = "LastBuildReports";
        private const string LibraryDirectoryName = "Library";
        private const string LastBuildReportFileName = "LastBuild.buildreport";
        private readonly BuildReport _buildReport;

        public BuildReportRepository()
        {
            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;
            if (string.IsNullOrEmpty(projectRootPath))
            {
                return;
            }

            var lastBuildReportPath = $"{Path.Combine(projectRootPath, LibraryDirectoryName, LastBuildReportFileName)}";
            if (!File.Exists(lastBuildReportPath))
            {
                return;
            }

            if (!Directory.Exists(_buildReportDir))
            {
                Directory.CreateDirectory(_buildReportDir);
            }

            File.Copy(lastBuildReportPath, _lastBuildReportsAssetPath, true);
            AssetDatabase.ImportAsset(_lastBuildReportsAssetPath);
            _buildReport = AssetDatabase.LoadAssetAtPath<BuildReport>(_lastBuildReportsAssetPath);
        }

        public void Dispose()
        {
            if (Directory.Exists(_buildReportDir))
            {
                Directory.Delete(_buildReportDir, true);
            }

            if (File.Exists($"{_buildReportDir}.meta"))
            {
                File.Delete($"{_buildReportDir}.meta");
            }
        }

        public BuildSummary GetBuildSummary()
        {
            return _buildReport.summary;
        }

        public BuildStep[] GetBuildSteps()
        {
            return _buildReport.steps;
        }

        public PackedAssets[] GetPackedAssets()
        {
            return _buildReport.packedAssets;
        }

        public IEnumerable<string> GetIncludedModuleNames()
        {
            return _buildReport.strippingInfo == null
                ? Array.Empty<string>()
                : _buildReport.strippingInfo.includedModules;
        }

        public IEnumerable<string> GetReasonsForIncluding(string entity)
        {
            return _buildReport.strippingInfo == null
                ? Array.Empty<string>()
                : _buildReport.strippingInfo.GetReasonsForIncluding(entity);
        }

        public BuildFile[] GetBuildFiles()
        {
#if UNITY_2022_1_OR_NEWER
            return _buildReport.GetFiles();
#else
            return _buildReport.files;
#endif
        }

        internal bool IsBuildReportActive()
        {
            return _buildReport != null;
        }
    }
}
