using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.ChangePassword
{
    internal class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public ChangePasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IExecutionContextAccessor executionContextAccessor)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(_executionContextAccessor.UserId);
            var user = await _userRepository.GetByIdAsync(userId);

            user.ChangePassword(request.OldPassword, request.NewPassword, _passwordHasher);
            
            return Unit.Value;
        }
    }
}