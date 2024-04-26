// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor
{
    internal sealed class BuildReportRepository : IDisposable
    {
        private readonly string _buildReportDir =
            $"{Path.Combine(Application.dataPath, LastBuildReportsDirectoryName)}";

        private readonly string _lastBuildReportsAssetPath =
            $"{Path.Combine("Assets", LastBuildReportsDirectoryName, LastBuildReportFileName)}";

        private const string LastBuildReportsDirectoryName = "LastBuildReports";
        private const string LibraryDirectoryName = "Library";
        private const string LastBuildReportFileName = "LastBuild.buildreport";

        [CanBeNull]
        internal BuildReport GetBuildReport()
        {
            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;
            if (string.IsNullOrEmpty(projectRootPath))
            {
                return null;
            }

            var lastBuildReportPath = $"{Path.Combine(projectRootPath, LibraryDirectoryName, LastBuildReportFileName)}";
            if (!File.Exists(lastBuildReportPath))
            {
                Debug.LogError($"{lastBuildReportPath} not exists");
                return null;
            }

            if (!Directory.Exists(_buildReportDir))
            {
                Directory.CreateDirectory(_buildReportDir);
            }

            File.Copy(lastBuildReportPath, _lastBuildReportsAssetPath, true);
            AssetDatabase.ImportAsset(_lastBuildReportsAssetPath);
            var report = AssetDatabase.LoadAssetAtPath<BuildReport>(_lastBuildReportsAssetPath);
            return report;
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
