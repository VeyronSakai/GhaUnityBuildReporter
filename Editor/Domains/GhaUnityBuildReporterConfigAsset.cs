// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Domains
{
    [CreateAssetMenu(fileName = "GhaUnityBuildReporterConfig", menuName = "GhaUnityBuildReporterConfig")]
    public class GhaUnityBuildReporterConfigAsset : ScriptableObject
    {
        public bool　basicInfo = true;
    }
}
