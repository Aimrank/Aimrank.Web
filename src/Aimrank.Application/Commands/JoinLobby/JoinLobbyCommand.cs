using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.JoinLobby
{
    public class JoinLobbyCommand : ICommand
    {
        public Guid Id { get; }

        public JoinLobbyCommand(Guid id)
        {
            Id = id;
        }
    }
}