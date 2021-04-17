using System;

namespace Aimrank.Web.Common.Application
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }
        bool IsAvailable { get; }
    }
}