using PM.WebApp.Infrastructure.Utils.Enums;

namespace PM.WebApp.Models
{
    public class Alert
    {
        public AlertMessageEnum Type { get; set; }
        public string Message { get; set; }
    }
}
