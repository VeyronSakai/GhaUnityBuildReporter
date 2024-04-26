using UnityEditor;

namespace Editor
{
    // ReSharper disable once UnusedMember.Global
    public sealed class BuildEntryPoint
    {
        // ReSharper disable once UnusedMember.Global
        public static void BuildAndroid()
        {
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Scenes/SampleScene.unity" },
                locationPathName = "Outputs/Android/app.apk",
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
                scenes = new[] { "Assets/Scenes/SampleScene.unity" },
                locationPathName = "Outputs/IOS/",
                target = BuildTarget.iOS,
                options = BuildOptions.Development,
            };

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}
