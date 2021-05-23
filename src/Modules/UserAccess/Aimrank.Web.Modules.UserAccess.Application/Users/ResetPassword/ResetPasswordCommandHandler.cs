using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.ResetPassword
{
    internal class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId);

            user.ResetPassword(request.NewPassword, request.Token, _passwordHasher);
            
            return Unit.Value;
        }
    }
}