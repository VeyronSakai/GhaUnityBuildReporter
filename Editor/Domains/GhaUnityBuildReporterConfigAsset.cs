// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [CreateAssetMenu(fileName = "GhaUnityBuildReporterConfig", menuName = "GhaUnityBuildReporterConfig")]
    public class GhaUnityBuildReporterConfigAsset : ScriptableObject
    {
        public bool title = true;
        public bool basicInfo = true;
        public bool buildSteps = true;
        public bool sourceAssets = true;
        public bool outputFiles = true;
        public bool includedModules = true;
    }
}
