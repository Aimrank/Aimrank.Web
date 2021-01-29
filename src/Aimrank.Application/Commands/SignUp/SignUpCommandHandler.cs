using Aimrank.Application.Commands.RefreshJwt;
using Aimrank.Application.Contracts;
using Aimrank.Domain.RefreshTokens;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.SignUp
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand, AuthenticationSuccessDto>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;

        public SignUpCommandHandler(
            UserManager<IdentityUser> userManager,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationSuccessDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            await AssertUniqueEmailAsync(request.Email);
            await AssertUniqueUsernameAsync(request.Username);

            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                try
                {
                    var refreshToken = RefreshToken.Create(user.Id, user.Email, _jwtService);
                    _refreshTokenRepository.Add(refreshToken);

                    return new AuthenticationSuccessDto
                    {
                        Jwt = refreshToken.Jwt,
                        RefreshToken = refreshToken.Id.Value.ToString()
                    };
                }
                catch
                {
                    await _userManager.DeleteAsync(user);
                    throw new SignUpException();
                }
            }

            throw new SignUpException();
        }
        
        private async Task AssertUniqueEmailAsync(string email)
        {
            var existsEmail = await _userManager.FindByEmailAsync(email);
            if (existsEmail is not null)
            {
                throw new SignUpException
                {
                    Errors =
                    {
                        ["Email"] = new List<string> {"This email is already taken"}
                    }
                };
            }
        }

        private async Task AssertUniqueUsernameAsync(string username)
        {
            var usernameExists = await _userManager.FindByNameAsync(username);
            if (usernameExists is not null)
            {
                throw new SignUpException
                {
                    Errors =
                    {
                        ["Username"] = new List<string> {"This username is already taken"}
                    }
                };
            }
        }
    }
}