using System;

namespace Aimrank.EventBus
{
    public class BusEventArgs : EventArgs
    {
        public string Content { get; }

        public BusEventArgs(string content) => Content = content;
    }
}
