// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [Serializable]
    internal class ReportConfig
    {
        [SerializeField] private bool writesTitle;
        [SerializeField] private bool writesBasicInfo;
        [SerializeField] private bool writesBuildSteps;
        [SerializeField] private bool writesSourceAssets;
        [SerializeField] private bool writesOutputFiles;
        [SerializeField] private bool writesIncludedModules;

        internal bool WritesTitle => writesTitle;
        internal bool WritesBasicInfo => writesBasicInfo;
        internal bool WritesBuildSteps => writesBuildSteps;
        internal bool WritesSourceAssets => writesSourceAssets;
        internal bool WritesOutputFiles => writesOutputFiles;
        internal bool WritesIncludedModules => writesIncludedModules;

        internal ReportConfig(
            bool writesTitle,
            bool writesBasicInfo,
            bool writesBuildSteps,
            bool writesSourceAssets,
            bool writesOutputFiles,
            bool writesIncludedModules
        )
        {
            this.writesTitle = writesTitle;
            this.writesBasicInfo = writesBasicInfo;
            this.writesBuildSteps = writesBuildSteps;
            this.writesSourceAssets = writesSourceAssets;
            this.writesOutputFiles = writesOutputFiles;
            this.writesIncludedModules = writesIncludedModules;
        }
    }
}
