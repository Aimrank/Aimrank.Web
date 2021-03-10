using FluentValidation;

namespace Aimrank.Application.Commands.Users.RefreshJwt
{
    internal class RefreshJwtCommandValidator : AbstractValidator<RefreshJwtCommand>
    {
        public RefreshJwtCommandValidator()
        {
            RuleFor(x => x.Jwt).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}