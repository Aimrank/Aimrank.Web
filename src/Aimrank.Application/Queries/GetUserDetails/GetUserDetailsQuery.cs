using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IQuery<UserDetailsDto>
    {
        public Guid UserId { get; }

        public GetUserDetailsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}