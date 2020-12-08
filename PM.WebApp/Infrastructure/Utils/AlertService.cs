using PM.WebApp.Infrastructure.Utils.Enums;
using PM.WebApp.Infrastructure.Utils.Interfaces;
using PM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public class AlertService : IAlertService
    {
        public List<Alert> Alerts { get; }
        public event Func<Task> RequestRefresh;

        public AlertService()
        {
            Alerts = new List<Alert>();
        }

        public void PushMessage(AlertMessageEnum type, string message, bool isAutoRemovable = true)
        {
            var alert = new Alert { Message = message, Type = type };

            Alerts.Add(alert);
            if (isAutoRemovable)
            {
                Task.Run(Remove(alert));
            }

            RequestRefresh?.Invoke();
        }

        private Action Remove(Alert alert)
        {
            return async () =>
            {
                await Task.Delay(3000);
                Alerts.Remove(alert);
                RequestRefresh?.Invoke();
            };
        }
    }
}
