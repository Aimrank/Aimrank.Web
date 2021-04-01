using Aimrank.Common.Application;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.UserAccess.Application.Users.ChangePassword
{
    internal class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public ChangePasswordCommandHandler(
            IUserRepository userRepository,
            IExecutionContextAccessor executionContextAccessor)
        {
            _userRepository = userRepository;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(_executionContextAccessor.UserId);
            var user = await _userRepository.GetByIdAsync(userId);

            var newPasswordHash = PasswordManager.HashPassword(request.NewPassword);

            user.ChangePassword(request.OldPassword, newPasswordHash);
            
            return Unit.Value;
        }
    }
}