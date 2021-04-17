using Aimrank.Web.Modules.UserAccess.Application.Authentication;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RegisterNewUser
{
    internal class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public RegisterNewUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(Guid.NewGuid());

            var password = PasswordManager.HashPassword(request.Password);

            var user = await User.CreateAsync(
                userId,
                request.Email,
                request.Username,
                password,
                _userRepository);

            _userRepository.Add(user);

            return userId.Value;
        }
    }
}