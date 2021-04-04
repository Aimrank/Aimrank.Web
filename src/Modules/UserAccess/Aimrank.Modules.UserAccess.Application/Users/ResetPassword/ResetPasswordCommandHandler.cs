using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.UserAccess.Application.Users.ResetPassword
{
    internal class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId);

            var newPasswordHash = PasswordManager.HashPassword(request.NewPassword);

            user.ResetPassword(newPasswordHash, request.Token);
            
            return Unit.Value;
        }
    }
}