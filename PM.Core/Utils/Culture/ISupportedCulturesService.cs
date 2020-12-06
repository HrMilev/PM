using System.Collections.Generic;

namespace PM.Common.Utils.Culture
{
    public interface ISupportedCulturesService
    {
        IList<string> Cultures { get; }
    }
}