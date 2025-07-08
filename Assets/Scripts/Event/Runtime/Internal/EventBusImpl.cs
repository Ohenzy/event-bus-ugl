using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace UGL.Event.Impl
{
    internal class EventBusImpl : IEventBus
    {
        private readonly Dictionary<Type, List<SubscriberEntry>> _actions = new();
        private readonly Dictionary<Object, List<Type>> _subscribers = new();

        public ISubscriptionBuilder For(Object subscriber)
        {
            return new SubscriptionBuilderImpl(subscriber, this);
        }

        public void Invoke<T>(T evt) where T : struct
        {
            var key = typeof(T);

            if (!_actions.TryGetValue(key, out var entries))
                return;

            foreach (var entry in entries)
            {
                if (entry.Delegate.Action is UnityAction<T> action)
                {
                    action.Invoke(evt);
                }
            }
        }

        public void Unsubscribe(Object subscriber)
        {
            if (!_subscribers.TryGetValue(subscriber, out var subscribedTypes))
            {
                return;
            }

            foreach (var type in subscribedTypes)
            {
                if (!_actions.TryGetValue(type, out var entries))
                {
                    continue;
                }

                entries.RemoveAll(e => e.Subscriber == subscriber);
                if (entries.Count == 0)
                {
                    _actions.Remove(type);
                }
            }

            _subscribers.Remove(subscriber);
        }

        public void UnsubscribeAll()
        {
            _actions.Clear();
            _subscribers.Clear();
        }

        internal void AddActions(Object subscriber, Dictionary<Type, List<PriorityDelegate>> actionsToAdd)
        {
            if (actionsToAdd.Count == 0)
            {
                return;
            }

            if (!_subscribers.ContainsKey(subscriber))
            {
                _subscribers[subscriber] = new List<Type>();
            }

            foreach (var (type, delegates) in actionsToAdd)
            {
                _subscribers[subscriber].Add(type);

                if (!_actions.ContainsKey(type))
                {
                    _actions[type] = new List<SubscriberEntry>();
                }

                foreach (var d in delegates)
                {
                    InsertSorted(_actions[type], new SubscriberEntry(subscriber, d));
                }
            }
        }

        private void InsertSorted(List<SubscriberEntry> list, SubscriberEntry entry)
        {
            var index = list.FindIndex(e => entry.Delegate.Priority < e.Delegate.Priority);
            if (index >= 0)
            {
                list.Insert(index, entry);
            }
            else
            {
                list.Add(entry);
            }
        }
    }
}