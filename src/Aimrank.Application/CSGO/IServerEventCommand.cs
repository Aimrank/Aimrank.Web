using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.CSGO
{
    public interface IServerEventCommand : ICommand
    {
        Guid ServerId { get; set; }
    }
}