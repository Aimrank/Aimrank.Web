using FluentValidation;

namespace Aimrank.Web.Contracts.Requests.Validators
{
    public class ExecuteCommandRequestValidator : AbstractValidator<ExecuteCommandRequest>
    {
        public ExecuteCommandRequestValidator()
        {
            RuleFor(x => x.Command).NotEmpty();
        }
    }
}