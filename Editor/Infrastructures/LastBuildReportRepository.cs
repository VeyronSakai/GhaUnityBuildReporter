// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.IO;
using GhaUnityBuildReporter.Editor.Domains;
using UnityEditor;
using UnityEngine;
using BuildReport = UnityEditor.Build.Reporting.BuildReport;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal sealed class LastBuildReportRepository : ILastBuildReportRepository
    {
        private const string LastBuildReportsDirectoryName = "LastBuildReports";
        private const string LibraryDirectoryName = "Library";
        private const string LastBuildReportFileName = "LastBuild.buildreport";

        private readonly string _buildReportDir =
            $"{Path.Combine(Application.dataPath, LastBuildReportsDirectoryName)}";

        private readonly BuildReport _lastBuildReport;

        private readonly string _lastBuildReportsAssetPath =
            $"{Path.Combine("Assets", LastBuildReportsDirectoryName, LastBuildReportFileName)}";

        public LastBuildReportRepository()
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
            _lastBuildReport = AssetDatabase.LoadAssetAtPath<BuildReport>(_lastBuildReportsAssetPath);
        }

        public BuildReport GetBuildReport()
        {
            return _lastBuildReport;
        }

        public IEnumerable<string> GetReasonsForIncluding(string entityName)
        {
            return _lastBuildReport.strippingInfo.GetReasonsForIncluding(entityName);
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
    }
}
