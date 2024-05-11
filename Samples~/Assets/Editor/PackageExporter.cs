// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Linq;
using UnityEditor;

namespace Editor
{
    public class PackageExporter
    {
        private const string PackagePath = "Packages/com.veyron-sakai.gha-unity-build-reporter/";
        private const string ExportPath = "./GhaUnityBuildReporter.unitypackage";

        [MenuItem("Tools/ExportPackage")]
        private static void Export()
        {
            var assetPathNames = AssetDatabase.FindAssets("", new[] { PackagePath })
                .Select(AssetDatabase.GUIDToAssetPath)
                .ToArray();

            AssetDatabase.ExportPackage(
                assetPathNames,
                ExportPath,
                ExportPackageOptions.IncludeDependencies);
        }
    }
}
