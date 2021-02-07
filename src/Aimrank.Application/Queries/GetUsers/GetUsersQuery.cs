using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.GetUserDetails;
using System.Collections.Generic;

namespace Aimrank.Application.Queries.GetUsers
{
    public class GetUsersQuery : IQuery<IEnumerable<UserDetailsDto>>
    {
        public string Name { get; }

        public GetUsersQuery(string name)
        {
            Name = name;
        }
    }
}