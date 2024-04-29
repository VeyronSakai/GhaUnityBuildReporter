// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly PackedAssets[] _packedAssets;

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

            if (_buildReport != null)
            {
                _packedAssets = _buildReport.packedAssets;
            }
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

        public ulong GetPackedAssetSize(int packedAssetIndex)
        {
            if (_packedAssets.Length == 0)
            {
                return 0;
            }

            if (_packedAssets.Length <= packedAssetIndex)
            {
                throw new IndexOutOfRangeException();
            }

            var packedAsset = _packedAssets[packedAssetIndex];
            var totalSize = packedAsset.contents.Aggregate<PackedAssetInfo, ulong>(0,
                (current, packedAssetContent) => current + packedAssetContent.packedSize);
            return totalSize;
        }

        public string GetPackAssetShortPath(int packedAssetIndex)
        {
            if (_packedAssets.Length <= packedAssetIndex)
            {
                throw new IndexOutOfRangeException();
            }

            return _packedAssets[packedAssetIndex].shortPath;
        }

        public int GetPackedAssetsCount()
        {
            return _packedAssets.Length;
        }

        public IEnumerable<PackedAssetInfo> GetPackedAssetContents(int packedAssetIndex)
        {
            if (_packedAssets.Length <= packedAssetIndex)
            {
                throw new IndexOutOfRangeException();
            }

            return _packedAssets[packedAssetIndex].contents;
        }

        internal bool IsBuildReportAvailable()
        {
            return _buildReport != null;
        }
    }
}
