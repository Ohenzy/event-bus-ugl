using System;
using UGL.Event.Impl;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGL.Event
{
    public class EventBusMono : MonoBehaviour, IEventBus
    {
        private readonly EventBusImpl _eventBus = new();

        public ISubscriptionBuilder For(Object owner)
        {
            return _eventBus.For(owner);
        }

        public void Invoke<T>(T evt) where T : struct, IEvent
        {
            _eventBus.Invoke(evt);
        }

        public void Unsubscribe(Object owner)
        {
            _eventBus.Unsubscribe(owner);
        }

        private void OnDestroy()
        {
            _eventBus.UnsubscribeAll();
        }
    }
}