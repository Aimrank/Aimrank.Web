using System;

namespace Aimrank.Application
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }
        bool IsAvailable { get; }
    }
}