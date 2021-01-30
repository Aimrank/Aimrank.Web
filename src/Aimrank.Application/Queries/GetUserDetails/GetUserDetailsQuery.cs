using Aimrank.Application.Contracts;

namespace Aimrank.Application.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IQuery<UserDetailsDto>
    {
        public string UserId { get; }

        public GetUserDetailsQuery(string userId)
        {
            UserId = userId;
        }
    }
}