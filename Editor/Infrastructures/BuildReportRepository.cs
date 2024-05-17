// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GhaUnityBuildReporter.Editor.Domains;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using BuildReport = GhaUnityBuildReporter.Editor.Domains.BuildReport;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal sealed class BuildReportRepository : AbstractBuildReportRepository
    {
        private const string LastBuildReportsDirectoryName = "LastBuildReports";
        private const string LibraryDirectoryName = "Library";
        private const string LastBuildReportFileName = "LastBuild.buildreport";

        private readonly string _buildReportDir =
            $"{Path.Combine(Application.dataPath, LastBuildReportsDirectoryName)}";

        private readonly string _lastBuildReportsAssetPath =
            $"{Path.Combine("Assets", LastBuildReportsDirectoryName, LastBuildReportFileName)}";

        [CanBeNull] private readonly UnityEditor.Build.Reporting.BuildReport _lastBuildReport;

        internal BuildReportRepository()
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
            _lastBuildReport =
                AssetDatabase.LoadAssetAtPath<UnityEditor.Build.Reporting.BuildReport>(_lastBuildReportsAssetPath);
        }

        internal override BuildReport GetBuildReport()
        {
            if (!_lastBuildReport)
            {
                return null;
            }

            var packedAssetsArray = GetPackedAssetsArray(_lastBuildReport);
            var includedModules = _lastBuildReport.strippingInfo == null
                ? Array.Empty<string>()
                : _lastBuildReport.strippingInfo.includedModules;
            var strippingInfo = new StrippingInfo(includedModules);
            var buildFiles = GetBuildFiles();
            var summary = new BuildSummary(
                _lastBuildReport.summary.platform,
                _lastBuildReport.summary.totalTime,
                _lastBuildReport.summary.totalSize,
                _lastBuildReport.summary.result,
                _lastBuildReport.summary.totalErrors,
                _lastBuildReport.summary.totalWarnings
            );

            var buildSteps = _lastBuildReport.steps.Select(originalBuildStep =>
            {
                var messages = originalBuildStep.messages.Select(originalMessage =>
                    new BuildStepMessage(originalMessage.type, originalMessage.content));

                return new BuildStep(originalBuildStep.name, originalBuildStep.duration, messages,
                    originalBuildStep.depth);
            }).ToArray();

            return new BuildReport(
                summary,
                buildSteps,
                packedAssetsArray,
                strippingInfo,
                buildFiles
            );
        }

        internal override IEnumerable<string> GetReasonsForIncluding(string entityName)
        {
            return _lastBuildReport == null
                ? Enumerable.Empty<string>()
                : _lastBuildReport.strippingInfo.GetReasonsForIncluding(entityName);
        }

        public override void Dispose()
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

        [NotNull]
        private static PackedAssets[] GetPackedAssetsArray(
            [NotNull] UnityEditor.Build.Reporting.BuildReport originalBuildReport)
        {
            var packedAssets = originalBuildReport.packedAssets.Select(originalPackedAssets =>
            {
                var packedAssetInfos = originalPackedAssets.contents.Select(content =>
                    new PackedAssetInfo(content.packedSize, content.sourceAssetPath));
                return new PackedAssets(originalPackedAssets.shortPath, packedAssetInfos);
            }).ToArray();

            return packedAssets;
        }

        [NotNull]
        private BuildFile[] GetBuildFiles()
        {
            var buildFiles =
#if UNITY_2022_1_OR_NEWER
                _lastBuildReport.GetFiles();
#else
                _lastBuildReport.files;
#endif

            return buildFiles.Select(file => new BuildFile(file.id, file.path, file.role, file.size)).ToArray();
        }
    }
}
