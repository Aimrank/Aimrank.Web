using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.ConfirmEmailAddress
{
    internal class ConfirmEmailAddressCommandHandler : ICommandHandler<ConfirmEmailAddressCommand>
    {
        private readonly IUserRepository _userRepository;

        public ConfirmEmailAddressCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ConfirmEmailAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(new UserId(request.UserId));
            
            user.ConfirmEmail(request.Token);
            
            return Unit.Value;
        }
    }
}