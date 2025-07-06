using UnityEngine.Events;

namespace UGL.Event
{
    public interface ISubscriptionBuilder
    {
        ISubscriptionBuilder Subscribe<T>(UnityAction<T> action) where T : struct, IEvent;
        
        void Build();
    }
}