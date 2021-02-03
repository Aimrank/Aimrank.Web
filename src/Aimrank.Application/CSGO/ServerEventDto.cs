using System;

namespace Aimrank.Application.CSGO
{
    public class ServerEventDto<T>
    {
        public Guid ServerId { get; set; }
        public string Name { get; set; }
        public T Data { get; set; }
    }
}