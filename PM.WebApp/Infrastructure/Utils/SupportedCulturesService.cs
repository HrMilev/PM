using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public class SupportedCulturesService
    {
        public IList<CultureInfo> Cultures { get; } = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("bg-BG"),
            };
    }
}
