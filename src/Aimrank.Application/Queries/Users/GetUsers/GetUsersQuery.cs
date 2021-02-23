using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;

namespace Aimrank.Application.Queries.Users.GetUsers
{
    public class GetUsersQuery : IQuery<IEnumerable<UserDto>>
    {
        public string Name { get; }

        public GetUsersQuery(string name)
        {
            Name = name;
        }
    }
}