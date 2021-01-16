using System;

namespace Aimrank.EventBus
{
    public interface IBusPublisher : IDisposable
    {
        void Publish(string content);
    }
}