using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserBatch;
using Aimrank.Common.Application.Queries;

namespace Aimrank.Application.Queries.Users.GetUsers
{
    public class GetUsersQuery : IQuery<PaginationDto<UserDto>>
    {
        public string Name { get; }
        public PaginationQuery Pagination { get; }

        public GetUsersQuery(string name, PaginationQuery pagination)
        {
            Name = name;
            Pagination = pagination;
        }
    }
}