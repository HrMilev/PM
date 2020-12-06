using PM.WebApp.Infrastructure.Utils.Enums;
using System;

namespace PM.WebApp.Infrastructure.Utils
{
    public interface IEventHandlerService
    {
        void Raise(EventHandlerEnum @event);
        void Subscribe(EventHandlerEnum @event, Action action);
    }
}