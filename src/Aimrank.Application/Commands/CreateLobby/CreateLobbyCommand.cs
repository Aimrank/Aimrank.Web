using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.CreateLobby
{
    public class CreateLobbyCommand : ICommand
    {
        public Guid Id { get; }

        public CreateLobbyCommand(Guid id)
        {
            Id = id;
        }
    }
}