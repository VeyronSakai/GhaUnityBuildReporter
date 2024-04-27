using UnityEditor;

namespace Editor
{
    // ReSharper disable once UnusedType.Global
    public sealed class BuildEntryPoint
    {
        private const string Scenes = "Assets/Scenes/SampleScene.unity";

        // ReSharper disable once UnusedMember.Global
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

        // ReSharper disable once UnusedMember.Global
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

        // ReSharper disable once UnusedMember.Global
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

        // ReSharper disable once UnusedMember.Global
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
