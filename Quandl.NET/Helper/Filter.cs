﻿using System.Collections.Generic;
using System.Linq;

namespace Quandl.NET.Helper
{
    internal static class Filter
    {
        public static Dictionary<string, string> Parse(string filter)
            => filter == null ? null : filter.Replace("<=", ".lte=")
                     .Replace(">=", ".gte=")
                     .Replace("<", ".lt=")
                     .Replace(">", ".gt=")
                     .Split('&')
                     .ToDictionary(ss => ss.Split('=')[0], ss => ss.Split('=')[1]);
    }
}