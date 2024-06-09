// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using UnityEditor;

namespace Editor
{
    public static class BuildEntryPoint
    {
        private const string Scenes = "Assets/Scenes/SampleScene.unity";

        [MenuItem("Tools/Build/Android")]
        public static void BuildAndroid()
        {
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] { Scenes },
                locationPathName = "Outputs/Android/Sample.apk",
                target = BuildTarget.Android,
                options = BuildOptions.Development,
            };

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        [MenuItem("Tools/Build/iOS")]
        public static void BuildIOS()
        {
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] { Scenes },
                locationPathName = "Outputs/IOS",
                target = BuildTarget.iOS,
                options = BuildOptions.Development,
            };

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        [MenuItem("Tools/Build/macOS")]
        public static void BuildMacOS()
        {
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] { Scenes },
                locationPathName = "Outputs/MacOS",
                target = BuildTarget.StandaloneOSX,
                options = BuildOptions.Development,
            };

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }


        [MenuItem("Tools/Build/Windows")]
        public static void BuildWindows()
        {
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] { Scenes },
                locationPathName = "Outputs/Windows.exe",
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.Development,
            };

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        [MenuItem("Tools/Build/WebGL")]
        public static void BuildWebGL()
        {
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] { Scenes },
                locationPathName = "Outputs/WebGL",
                target = BuildTarget.WebGL,
                options = BuildOptions.Development,
            };

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}
