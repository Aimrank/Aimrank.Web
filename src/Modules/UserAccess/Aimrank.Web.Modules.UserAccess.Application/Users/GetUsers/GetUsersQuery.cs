using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Users.GetUserBatch;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.GetUsers
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