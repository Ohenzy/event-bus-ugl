using UnityEngine.Events;

namespace UGL.Event
{
    public interface ISubscriptionBuilder
    {
        ISubscriptionBuilder Subscribe<T>(UnityAction<T> action, byte priority = byte.MaxValue) where T : struct;

        IEventBus Build();
    }
}