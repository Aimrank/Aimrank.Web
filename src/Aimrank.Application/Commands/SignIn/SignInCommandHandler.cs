using Aimrank.Application.Commands.RefreshJwt;
using Aimrank.Application.Contracts;
using Aimrank.Domain.RefreshTokens;
using Aimrank.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.SignIn
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand, AuthenticationSuccessDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;

        public SignInCommandHandler(
            UserManager<User> userManager,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationSuccessDto> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user =
                (await _userManager.FindByEmailAsync(request.UsernameOrEmail)) ??
                (await _userManager.FindByNameAsync(request.UsernameOrEmail));

            if (user is null)
            {
                throw new SignInException();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (passwordValid)
            {
                var refreshToken = RefreshToken.Create(user.Id, user.Email, _jwtService);
                _refreshTokenRepository.Add(refreshToken);

                return new AuthenticationSuccessDto
                {
                    Jwt = refreshToken.Jwt,
                    RefreshToken = refreshToken.Id.Value.ToString()
                };
            }

            throw new SignInException();
        }
    }
}