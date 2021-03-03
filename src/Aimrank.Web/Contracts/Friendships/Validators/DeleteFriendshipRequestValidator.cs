using FluentValidation;

namespace Aimrank.Web.Contracts.Friendships.Validators
{
    public class DeleteFriendshipRequestValidator : AbstractValidator<DeleteFriendshipRequest>
    {
        public DeleteFriendshipRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}