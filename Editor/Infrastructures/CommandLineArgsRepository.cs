// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using System;
using System.Collections.Generic;
using GhaUnityBuildReporter.Editor.Domains;

namespace GhaUnityBuildReporter.Editor.Infrastructures
{
    internal class CommandLineArgsRepository : AbstractCommandLineArgsRepository
    {
        private readonly Dictionary<string, string> _dict = new();

        internal CommandLineArgsRepository()
        {
            var args = Environment.GetCommandLineArgs();
            for (var i = 0; i < args.Length; i++)
            {
                if (!args[i].StartsWith('-'))
                {
                    continue;
                }

                if (i + 1 < args.Length && !args[i + 1].StartsWith('-'))
                {
                    _dict.Add(args[i], args[i + 1]);
                }
                else
                {
                    _dict.Add(args[i], string.Empty);
                }
            }
        }

        internal string GetValue(string key)
        {
            return Has(key) ? _dict[key] : string.Empty;
        }

        private bool Has(string key)
        {
            return _dict.ContainsKey(key);
        }
    }
}
