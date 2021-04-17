using System.Collections.Generic;
using System;

namespace Aimrank.Web.Common.Application.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public string Code => "command_validation_failed";

        public Dictionary<string, List<string>> Errors { get; }

        public InvalidCommandException(Dictionary<string, List<string>> errors = null)
            : base("Command validation failed")
        {
            Errors = errors ?? new Dictionary<string, List<string>>();
        }
    }
}