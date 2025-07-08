using UnityEngine;

namespace UGL.Event
{
    public interface IEventBus
    {
        ISubscriptionBuilder For(Object subscriber);

        void Unsubscribe(Object subscriber);
        
        void Invoke<T>(T evt) where T : struct;
    } 
}