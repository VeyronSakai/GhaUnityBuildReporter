// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    [CreateAssetMenu(fileName = "GhaUnityBuildReporterConfig", menuName = "GhaUnityBuildReporterConfig")]
    public class GhaUnityBuildReporterConfigAsset : ScriptableObject
    {
        [SerializeField] internal bool title = true;
        [SerializeField] internal bool basicInfo = true;
        [SerializeField] internal bool buildSteps = true;
        [SerializeField] internal bool sourceAssets;
        [SerializeField] internal bool outputFiles;
        [SerializeField] internal bool includedModules = true;
    }
}
