using UGL.Event;

namespace Example
{
    public readonly struct CountEvent : IEvent
    {
        public readonly int CountValue;

        public CountEvent(int value)
        {
            CountValue = value;
        }
    }
}