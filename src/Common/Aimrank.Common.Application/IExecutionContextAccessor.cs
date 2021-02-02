using System;

namespace Aimrank.Common.Application
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }
        bool IsAvailable { get; }
    }
}