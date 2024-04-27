// Copyright (c) 2020-2024 VeyronSakai.
// This software is released under the MIT License.

using System.Threading;
using System.Threading.Tasks;

namespace GhaUnityBuildReporter.Editor
{
    internal interface IJobSummaryRepository
    {
        ValueTask AppendTextAsync(string text, CancellationToken cancellationToken);
    }
}
