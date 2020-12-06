using PM.WebApp.Infrastructure.Utils.Enums;
using PM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public interface IAlertService
    {
        List<Alert> Alerts { get; }

        event Func<Task> RequestRefresh;

        void PushMessage(AlertMessageEnum type, string message);
    }
}