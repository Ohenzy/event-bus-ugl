using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace UGL.Event.Impl
{
    internal class SubscriptionBuilderImpl : ISubscriptionBuilder
    {
        private readonly EventBusImpl _eventBus;
        private readonly Object _owner;
        private readonly Dictionary<Type, List<Delegate>> _actions = new();

        public SubscriptionBuilderImpl(Object owner, EventBusImpl eventBus)
        {
            _eventBus = eventBus;
            _owner = owner;
        }

        public ISubscriptionBuilder Subscribe<T>(UnityAction<T> action) where T : struct, IEvent
        {
            var key = typeof(T);
            if (!_actions.ContainsKey(key))
            {
                _actions[key] = new List<Delegate>();
            }
            _actions[key].Add(action);
            return this;
        }

        public void Build()
        {
            _eventBus.AddActions(_owner, _actions);
        }
    }
}