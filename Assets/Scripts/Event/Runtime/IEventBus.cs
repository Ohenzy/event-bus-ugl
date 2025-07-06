using UnityEngine;

namespace UGL.Event
{
    public interface IEventBus
    {
        ISubscriptionBuilder For(Object owner);

        void Unsubscribe(Object owner);
        
        void Invoke<T>(T evt) where T : struct, IEvent;
    } 
}