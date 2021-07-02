using System.Collections.Generic;
using System.Globalization;

namespace PM.Common.Utils.Culture
{
    public interface ISupportedCulturesService
    {
        IList<string> Names { get; }
    }
}