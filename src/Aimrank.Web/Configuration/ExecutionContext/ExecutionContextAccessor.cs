using Aimrank.Application;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace Aimrank.Web.Configuration.ExecutionContext
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type == "id");
                if (claim?.Value is null)
                {
                    throw new ExecutionContextNotAvailableException();
                }

                return Guid.Parse(claim.Value);
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext is not null;
    }
}