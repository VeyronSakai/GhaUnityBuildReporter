// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor;
using UnityEngine;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    [FilePath("ProjectSettings/GhaUnityBuildReporter.asset", FilePathAttribute.Location.ProjectFolder)]
    internal sealed class ProjectSettingSingleton : ScriptableSingleton<ProjectSettingSingleton>
    {
        [SerializeField] internal bool title = true;
        [SerializeField] internal bool basicInfo = true;
        [SerializeField] internal bool buildSteps = true;
        [SerializeField] internal bool sourceAssets;
        [SerializeField] internal bool outputFiles;
        [SerializeField] internal bool includedModules = true;

        internal void Save()
        {
            base.Save(true);
        }
    }
}
