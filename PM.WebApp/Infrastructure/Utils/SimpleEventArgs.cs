using PM.WebApp.Infrastructure.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public class SimpleEventArgs
    {
        public SimpleEventArgs(EventHandlerEnum handlerEnum)
        {
            Type = handlerEnum;
        }

        public EventHandlerEnum Type { get; }

        public bool IsOfType(params EventHandlerEnum[] eventHandlers)
        {
            return eventHandlers.Contains(Type);
        }
    }
}
