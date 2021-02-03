using Aimrank.Application.Contracts;

namespace Aimrank.Application.Commands.ProcessServerEvent
{
    public class ProcessServerEventCommand : ICommand
    {
        public string Content { get; }

        public ProcessServerEventCommand(string content)
        {
            Content = content;
        }
    }
}