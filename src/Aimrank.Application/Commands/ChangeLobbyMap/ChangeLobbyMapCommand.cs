using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.ChangeLobbyMap
{
    public class ChangeLobbyMapCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }

        public ChangeLobbyMapCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}