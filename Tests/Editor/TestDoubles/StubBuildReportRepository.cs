// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.Linq;
using GhaUnityBuildReporter.Editor.Domains;

namespace GhaUnityBuildReporter.Editor.Tests.TestDoubles
{
    internal class StubBuildReportRepository : AbstractBuildReportRepository
    {
        internal override BuildReport GetBuildReport()
        {
            return Helper.GenerateStubBuildReport();
        }

        internal override IEnumerable<string> GetReasonsForIncluding(string entityName)
        {
            return entityName switch
            {
                "Animation Module" => new[] { "Animator", "AnimatorController", "AnimatorOverrideController" },
                "Audio Module" => new[] { "AudioBehaviour", "AudioClip", "AudioListener" },
                "AudioBehaviour" => new[] { "Assets/Scenes/SampleScene.unity" },
                "AudioListener" => new[] { "Assets/Scenes/SampleScene.unity" },
                _ => Enumerable.Empty<string>()
            };
        }

        public override void Dispose()
        {
        }
    }
}
