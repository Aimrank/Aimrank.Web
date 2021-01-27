using FluentValidation;
using System.Linq;

namespace Aimrank.Web.Contracts.Requests.Validators
{
    public class CreateServerRequestValidator : AbstractValidator<CreateServerRequest>
    {
        public CreateServerRequestValidator()
        {
            RuleFor(x => x.Token).NotEmpty().MaximumLength(32);
            RuleFor(x => x.Map)
                .NotEmpty()
                .Must(m => m == "aim_map" || m == "aim_redline")
                .WithMessage("Provided map name is not supported");
            RuleFor(x => x.Whitelist)
                .NotEmpty()
                .Must(w => w is not null && w.Length > 0 && w.All(id => id.Length == 17))
                .WithMessage("List contains invalid SteamID64");
        }
    }
}