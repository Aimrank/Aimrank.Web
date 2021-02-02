using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.CloseLobby
{
    public class CloseLobbyCommand : ICommand
    {
        public Guid Id { get; }

        public CloseLobbyCommand(Guid id)
        {
            Id = id;
        }
    }
}