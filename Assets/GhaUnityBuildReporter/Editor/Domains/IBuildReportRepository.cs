// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System.Collections.Generic;
using UnityEditor.Build.Reporting;

namespace GhaUnityBuildReporter.Editor.Domains
{
    public interface IBuildReportRepository
    {
        BuildSummary GetBuildSummary();
        BuildStep[] GetBuildSteps();
        IEnumerable<string> GetIncludedModuleNames();
        IEnumerable<string> GetReasonsForIncluding(string entity);
        BuildFile[] GetBuildFiles();
        ulong GetPackedAssetSize(int packedAssetIndex);
        int GetPackedAssetsCount();
        IEnumerable<PackedAssetInfo> GetPackedAssetContents(int packedAssetIndex);
        string GetPackAssetShortPath(int packedAssetIndex);
    }
}
