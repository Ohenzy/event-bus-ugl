namespace Example
{
    public readonly struct CountEvent
    {
        public readonly int CountValue;

        public CountEvent(int value)
        {
            CountValue = value;
        }
    }
}