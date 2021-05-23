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
        private readonly IPasswordHasher _passwordHasher;

        public RegisterNewUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(Guid.NewGuid());

            var user = await User.CreateAsync(
                userId,
                request.Email,
                request.Username,
                request.Password,
                _userRepository,
                _passwordHasher);

            _userRepository.Add(user);

            return userId.Value;
        }
    }
}