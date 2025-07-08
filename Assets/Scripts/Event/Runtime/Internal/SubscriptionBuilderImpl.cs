using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace UGL.Event.Impl
{
    internal class SubscriptionBuilderImpl : ISubscriptionBuilder
    {
        private readonly Object _subscriber;
        private readonly EventBusImpl _eventBus;
        private readonly Dictionary<Type, List<PriorityDelegate>> _actions = new();

        public SubscriptionBuilderImpl(Object subscriber, EventBusImpl eventBus)
        {
            _subscriber = subscriber;
            _eventBus = eventBus;
        }

        public ISubscriptionBuilder Subscribe<T>(UnityAction<T> action, byte priority = byte.MaxValue) where T : struct
        {
            var type = typeof(T);
            if (!_actions.ContainsKey(type))
            {
                _actions[type] = new List<PriorityDelegate>();
            }

            _actions[type].Add(new PriorityDelegate(action, priority));
            return this;
        }

        public void Build()
        {
            _eventBus.AddActions(_subscriber, _actions);
        }
    }
}