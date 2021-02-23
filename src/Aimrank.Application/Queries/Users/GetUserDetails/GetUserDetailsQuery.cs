using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.Users.GetUserDetails
{
    public class GetUserDetailsQuery : IQuery<UserDto>
    {
        public Guid UserId { get; }

        public GetUserDetailsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}