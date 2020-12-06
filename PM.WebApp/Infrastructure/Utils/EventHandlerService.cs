using PM.WebApp.Infrastructure.Utils.Enums;
using System;
using System.Collections.Generic;

namespace PM.WebApp.Infrastructure.Utils
{
    public class EventHandlerService : IEventHandlerService
    {
        private IDictionary<EventHandlerEnum, IList<Action>> _events;

        public EventHandlerService()
        {
            _events = new Dictionary<EventHandlerEnum, IList<Action>>();
        }

        public void Subscribe(EventHandlerEnum @event, Action action)
        {
            if (!_events.ContainsKey(@event))
            {
                _events[@event] = new List<Action>();
            }

            _events[@event].Add(action);
        }

        public void Raise(EventHandlerEnum @event)
        {
            if (_events.ContainsKey(@event))
            {
                foreach (var action in _events[@event])
                {
                    action();
                }
            }
        }
    }
}
