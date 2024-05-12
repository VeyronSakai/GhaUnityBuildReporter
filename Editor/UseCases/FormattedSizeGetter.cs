// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

namespace GhaUnityBuildReporter.Editor.UseCases
{
    internal static class FormattedSizeGetter
    {
        internal static string Get(ulong size)
        {
            return size switch
            {
                < 1024 => size + " B",
                < 1024 * 1024 => (size / 1024.00).ToString("F2") + " KB",
                < 1024 * 1024 * 1024 => (size / (1024.0 * 1024.0)).ToString("F2") + " MB",
                _ => (size / (1024.0 * 1024.0 * 1024.0)).ToString("F2") + " GB"
            };
        }
    }
}
