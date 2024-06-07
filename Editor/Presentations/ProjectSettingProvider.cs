// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;
using GhaUnityBuildReporter.Editor.Infrastructures;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GhaUnityBuildReporter.Editor.Presentations
{
    internal sealed class ProjectSettingProvider : SettingsProvider
    {
        private UnityEditor.Editor _editor;
        private const string SettingPath = "Project/GhaUnityBuildReporter";

        private ProjectSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(
            path, scopes, keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);

            var projectSetting = ProjectSettingSingleton.instance;
            projectSetting.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;
            UnityEditor.Editor.CreateCachedEditor(projectSetting, null, ref _editor);
        }

        [SettingsProvider]
        internal static SettingsProvider CreateSettingProvider()
        {
            return new ProjectSettingProvider(SettingPath, SettingsScope.Project,
                new[] { "GhaUnityBuildReporter", "Gha", "GHA", "GitHubActions" });
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUI.BeginChangeCheck();

            _editor.OnInspectorGUI();

            if (EditorGUI.EndChangeCheck())
            {
                ProjectSettingSingleton.instance.Save();
            }
        }
    }
}
