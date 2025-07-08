using UGL.Event.Impl;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGL.Event
{
    public class EventBusMono : MonoBehaviour, IEventBus
    {
        private readonly EventBusImpl _eventBus = new();

        public ISubscriptionBuilder For(Object subscriber)
        {
            return _eventBus.For(subscriber);
        }

        public void Invoke<T>(T evt) where T : struct
        {
            _eventBus.Invoke(evt);
        }

        public void Unsubscribe(Object subscriber)
        {
            _eventBus.Unsubscribe(subscriber);
        }

        private void OnDestroy()
        {
            _eventBus.UnsubscribeAll();
        }
    }
}