using Object = UnityEngine.Object;

namespace UGL.Event
{
    internal readonly struct SubscriberEntry
    {
        public readonly Object Subscriber;
        public readonly PriorityDelegate Delegate;

        public SubscriberEntry(Object subscriber, PriorityDelegate priorityDelegate)
        {
            Subscriber = subscriber;
            Delegate = priorityDelegate;
        }
    }
}