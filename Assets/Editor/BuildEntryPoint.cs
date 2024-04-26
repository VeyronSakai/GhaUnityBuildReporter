using UnityEditor;

namespace Editor
{
    public sealed class BuildEntryPoint
    {
        public static void Build()
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
    }
}
