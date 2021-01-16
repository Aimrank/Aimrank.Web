using System;

namespace Aimrank.EventBus
{
    public interface IBusReceiver
    {
        event EventHandler<BusEventArgs> MessageReceived;
        void Listen();
    }
}