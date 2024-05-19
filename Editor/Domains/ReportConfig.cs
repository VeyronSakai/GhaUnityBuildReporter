// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

namespace GhaUnityBuildReporter.Editor.Domains
{
    internal sealed class ReportConfig
    {
        internal bool WritesTitle { get; }
        internal bool WritesBasicInfo { get; }
        internal bool WritesBuildSteps { get; }
        internal bool WritesSourceAssets { get; }
        internal bool WritesOutputFiles { get; }
        internal bool WritesIncludedModules { get; }

        internal ReportConfig(
            bool writesTitle,
            bool writesBasicInfo,
            bool writesBuildSteps,
            bool writesSourceAssets,
            bool writesOutputFiles,
            bool writesIncludedModules
        )
        {
            WritesTitle = writesTitle;
            WritesBasicInfo = writesBasicInfo;
            WritesBuildSteps = writesBuildSteps;
            WritesSourceAssets = writesSourceAssets;
            WritesOutputFiles = writesOutputFiles;
            WritesIncludedModules = writesIncludedModules;
        }
    }
}
