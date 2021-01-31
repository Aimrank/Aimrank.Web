using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.LeaveLobby
{
    public class LeaveLobbyCommand : ICommand
    {
        public Guid Id { get; }

        public LeaveLobbyCommand(Guid id)
        {
            Id = id;
        }
    }
}