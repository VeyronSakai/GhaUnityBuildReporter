// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using GhaUnityBuildReporter.Editor.Domains;

namespace GhaUnityBuildReporter.Editor.Tests.TestDoubles
{
    internal class StubReportConfigRepository : AbstractReportConfigRepository
    {
        private readonly bool _existsConfig;
        private readonly bool _writesBasicInfo;

        internal StubReportConfigRepository(bool existsConfig = true, bool writesBasicInfo = true)
        {
            _existsConfig = existsConfig;
            _writesBasicInfo = writesBasicInfo;
        }

        internal override ReportConfig GetReporterConfig(string configPath)
        {
            return _existsConfig ? new ReportConfig(_writesBasicInfo) : null;
        }
    }
}
