using PM.WebApp.Infrastructure.Utils.Enums;
using System;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public class EventHandlerService : IEventHandlerService
    {
        public event Func<SimpleEventArgs, Task> OnNotify;

        public async void RaiseAsync(EventHandlerEnum @event)
        {
            var simpleEventArgs = new SimpleEventArgs(@event);
            await OnNotify?.Invoke(simpleEventArgs);
        }
    }
}
