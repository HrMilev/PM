﻿using System.Collections.Generic;
using System.Globalization;

namespace PM.WebApp.StaticData
{
    public static class SupportedCultures
    {
        public static IList<CultureInfo> Cultures { get; } = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("bg-BG"),
            };
    }
}
