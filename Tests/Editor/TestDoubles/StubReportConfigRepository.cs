// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using GhaUnityBuildReporter.Editor.Domains;
using GhaUnityBuildReporter.Editor.Infrastructures;

namespace GhaUnityBuildReporter.Editor.Tests.TestDoubles
{
    internal class StubReportConfigRepository : AbstractReportConfigRepository
    {
        private readonly bool _existsConfig;
        private readonly bool _writesTitle;
        private readonly bool _writesBasicInfo;
        private readonly bool _writesBuildSteps;
        private readonly bool _writesSourceAssets;
        private readonly bool _writesOutputFiles;
        private readonly bool _writesIncludedModules;

        internal StubReportConfigRepository(
            bool existsConfig = true,
            bool writesTitle = true,
            bool writesBasicInfo = true,
            bool writesBuildSteps = true,
            bool writesSourceAssets = true,
            bool writesOutputFiles = true,
            bool writesIncludedModules = true
        )
        {
            _existsConfig = existsConfig;
            _writesTitle = writesTitle;
            _writesBasicInfo = writesBasicInfo;
            _writesBuildSteps = writesBuildSteps;
            _writesSourceAssets = writesSourceAssets;
            _writesOutputFiles = writesOutputFiles;
            _writesIncludedModules = writesIncludedModules;
        }

        internal override ReportConfig GetReporterConfig()
        {
            if (_existsConfig)
            {
                return new ReportConfig(_writesTitle, _writesBasicInfo, _writesBuildSteps, _writesSourceAssets,
                    _writesOutputFiles, _writesIncludedModules);
            }

            return new ReportConfig(
                true,
                true,
                true,
                false,
                false,
                true
            );
        }
    }
}
