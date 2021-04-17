using FluentValidation;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.CreatePod
{
    public class CreatePodCommandValidator : AbstractValidator<CreatePodCommand>
    {
        public CreatePodCommandValidator()
        {
            RuleFor(x => x.IpAddress).NotEmpty();
            RuleFor(x => x.MaxServers).GreaterThan(0);
        }
    }
}