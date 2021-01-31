using Aimrank.Application.Commands.RefreshJwt;
using Aimrank.Application.Contracts;
using Aimrank.Application.Services;
using Aimrank.Domain.RefreshTokens;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.SignIn
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand, AuthenticationSuccessDto>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;

        public SignInCommandHandler(
            IAuthenticationService authenticationService,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService)
        {
            _authenticationService = authenticationService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationSuccessDto> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user =
                (await _authenticationService.AuthenticateUserWithEmailAsync(request.UsernameOrEmail, request.Password)) ??
                (await _authenticationService.AuthenticateUserWithUsernameAsync(request.UsernameOrEmail, request.Password));

            if (user is null)
            {
                throw new SignInException();
            }
            
            var refreshToken = RefreshToken.Create(user.Id, user.Email, _jwtService);
            _refreshTokenRepository.Add(refreshToken);

            return new AuthenticationSuccessDto
            {
                Jwt = refreshToken.Jwt,
                RefreshToken = refreshToken.Id.Value.ToString()
            };
        }
    }
}