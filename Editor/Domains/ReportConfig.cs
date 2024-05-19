// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class ReportConfig
    {
        internal bool WritesBasicInfo { get; }

        internal ReportConfig(bool writesBasicInfo)
        {
            WritesBasicInfo = writesBasicInfo;
        }
    }
}
