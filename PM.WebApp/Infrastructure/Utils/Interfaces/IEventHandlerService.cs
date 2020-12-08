using PM.WebApp.Infrastructure.Utils.Enums;
using System;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public interface IEventHandlerService
    {
        void RaiseAsync(EventHandlerEnum @event);
        void Subscribe(EventHandlerEnum @event, Func<Task> asyncAction);
    }
}