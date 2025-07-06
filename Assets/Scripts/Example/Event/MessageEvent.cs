using UGL.Event;

namespace Example
{
    public readonly struct MessageEvent : IEvent
    {
        public readonly string Message;

        public MessageEvent(string message)
        {
            Message = message;
        }
    }
}