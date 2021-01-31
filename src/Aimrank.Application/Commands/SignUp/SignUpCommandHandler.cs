using Aimrank.Application.Commands.RefreshJwt;
using Aimrank.Application.Contracts;
using Aimrank.Common.Domain;
using Aimrank.Domain.RefreshTokens;
using Aimrank.Domain.Users.Rules;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Commands.SignUp
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand, AuthenticationSuccessDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public SignUpCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationSuccessDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = await CreateUserAsync(request.Email, request.Username);

            var refreshToken = RefreshToken.Create(user.Id, user.Email, _jwtService);
            
            _refreshTokenRepository.Add(refreshToken);
            
            var result = await _userRepository.AddAsync(user, request.Password);
            if (result)
            {
                return new AuthenticationSuccessDto
                {
                    Jwt = refreshToken.Jwt,
                    RefreshToken = refreshToken.Id.Value.ToString()
                };
            }

            throw new SignUpException();
        }

        private async Task<User> CreateUserAsync(string email, string username)
        {
            var userId = new UserId(Guid.NewGuid());
            
            try
            {
                var user = await User.CreateAsync(userId, email, username, _userRepository);
                return user;
            }
            catch (BusinessRuleValidationException exception)
            {
                switch (exception.BrokenRule)
                {
                    case UsernameMustBeUniqueRule:
                        throw new SignUpException
                            {Errors = {["Username"] = new List<string> {exception.BrokenRule.Message}}};
                    case EmailMustBeUniqueRule:
                        throw new SignUpException
                            {Errors = {["Email"] = new List<string> {exception.BrokenRule.Message}}};
                    default:
                        throw;
                }
            }
        }
    }
}