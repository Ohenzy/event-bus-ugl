using System;

namespace UGL.Event
{
    internal readonly struct PriorityDelegate
    {
        public readonly Delegate Action;
        public readonly byte Priority;

        public PriorityDelegate(Delegate action, byte priority)
        {
            Action = action;
            Priority = priority;
        }
    }
}