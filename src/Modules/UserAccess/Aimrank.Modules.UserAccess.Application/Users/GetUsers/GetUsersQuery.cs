using Aimrank.Common.Application.Queries;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Users.GetUserBatch;

namespace Aimrank.Modules.UserAccess.Application.Users.GetUsers
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