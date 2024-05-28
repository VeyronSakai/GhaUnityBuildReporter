// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor;

namespace GhaUnityBuildReporter.Editor.Presentations
{
    [FilePath("ProjectSettings/GhaUnityBuildReporter.asset", FilePathAttribute.Location.ProjectFolder)]
    internal sealed class ProjectSetting : ScriptableSingleton<ProjectSetting>
    {
        public bool flag;
        public string text;

        internal void Save()
        {
            Save(true);
        }
    }
}
