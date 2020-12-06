using System.Collections.Generic;
using System.Globalization;

namespace PM.Common.Utils.Culture
{
    public class SupportedCulturesService : ISupportedCulturesService
    {
        public IList<string> Cultures { get; } = new List<string>
            {
                "en-US",
                "bg-BG",
            };
    }
}
