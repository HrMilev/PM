using PM.WebApp.Infrastructure.Utils.Enums;
using System;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public interface IEventHandlerService
    {
        event Func<SimpleEventArgs, Task> OnNotify;
        void RaiseAsync(EventHandlerEnum @event);
    }
}