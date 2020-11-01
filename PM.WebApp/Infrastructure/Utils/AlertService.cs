using PM.WebApp.Infrastructure.Utils.Enums;
using PM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public class AlertService
    {
        public List<Alert> Alerts { get; }
        public event Func<Task> RequestRefresh;

        public AlertService()
        {
            Alerts = new List<Alert>();
        }

        public async void PushMessage(AlertMessageEnum type, string message)
        {
            var alert = new Alert { Message = message, Type = type };

            Task.Run(async () =>
            {
                await Task.Delay(3000);
                Alerts.Remove(alert);
                RequestRefresh?.Invoke();
            });

            Alerts.Add(alert);
            await RequestRefresh?.Invoke();
        }
    }
}
