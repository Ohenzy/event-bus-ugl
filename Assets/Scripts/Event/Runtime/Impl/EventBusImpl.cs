using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace UGL.Event.Impl
{
    internal class EventBusImpl : IEventBus
    {
        private readonly Dictionary<Object, List<Type>> _owners = new();
        private readonly Dictionary<Type, List<Delegate>> _actions = new();

        public ISubscriptionBuilder For(Object owner)
        {
            return new SubscriptionBuilderImpl(owner, this);
        }

        public void Invoke<T>(T evt) where T : struct, IEvent
        {
            var key = typeof(T);

            if (!_actions.TryGetValue(key, out var delegates))
            {
                return;
            }

            foreach (var del in delegates)
            {
                if (del is UnityAction<T> action)
                {
                    action(evt);
                }
            }
        }

        public void Unsubscribe(Object owner)
        {
            if (!_owners.TryGetValue(owner, out var ownerSubscribes))
            {
                return;
            }

            foreach (var key in ownerSubscribes)
            {
                _actions.Remove(key);
            }

            _owners.Remove(owner);
        }

        public void UnsubscribeAll()
        {
            _owners.Clear();
            _actions.Clear();
        }

        internal void AddActions(Object owner, Dictionary<Type, List<Delegate>> actions)
        {
            if (actions.Count == 0)
            {
                return;
            }

            if (!_owners.ContainsKey(owner))
            {
                _owners[owner] = new List<Type>();
            }

            _owners[owner].AddRange(actions.Keys);
            AddActions(actions);
        }

        private void AddActions(Dictionary<Type, List<Delegate>> actions)
        {
            foreach (var (key, newDelegates) in actions)
            {
                if (!_actions.ContainsKey(key))
                {
                    _actions[key] = new List<Delegate>();
                }

                _actions[key].AddRange(newDelegates);
            }
        }
    }
}