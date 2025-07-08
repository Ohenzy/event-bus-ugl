namespace Example
{
    public readonly struct MessageEvent
    {
        public readonly string Message;

        public MessageEvent(string message)
        {
            Message = message;
        }
    }
}