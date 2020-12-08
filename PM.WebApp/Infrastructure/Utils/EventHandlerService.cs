using PM.WebApp.Infrastructure.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PM.WebApp.Infrastructure.Utils
{
    public class EventHandlerService : IEventHandlerService
    {
        private IDictionary<EventHandlerEnum, IList<Func<Task>>> _events;

        public EventHandlerService()
        {
            _events = new Dictionary<EventHandlerEnum, IList<Func<Task>>>();
        }

        public void Subscribe(EventHandlerEnum @event, Func<Task> asyncAction)
        {
            if (!_events.ContainsKey(@event))
            {
                _events[@event] = new List<Func<Task>>();
            }

            _events[@event].Add(asyncAction);
        }

        public async void RaiseAsync(EventHandlerEnum @event)
        {
            if (_events.ContainsKey(@event))
            {
                foreach (var action in _events[@event])
                {
                    await action();
                }
            }
        }
    }
}
