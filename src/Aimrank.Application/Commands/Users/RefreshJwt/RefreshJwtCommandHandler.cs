using Aimrank.Application.Contracts;
using Aimrank.Domain.RefreshTokens;
using System.Threading.Tasks;
using System.Threading;
using Aimrank.Application.Commands.Users.SignIn;

namespace Aimrank.Application.Commands.Users.RefreshJwt
{
    internal class RefreshJwtCommandHandler : ICommandHandler<RefreshJwtCommand, AuthenticationSuccessDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;

        public RefreshJwtCommandHandler(IRefreshTokenRepository refreshTokenRepository, IJwtService jwtService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationSuccessDto> Handle(RefreshJwtCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(new RefreshTokenId(request.RefreshToken), request.Jwt);
            if (refreshToken is null)
            {
                throw new SignInException();
            }

            var userId = _jwtService.GetUserId(request.Jwt);
            var userEmail = _jwtService.GetUserEmail(request.Jwt);
            
            refreshToken.Refresh(_jwtService.CreateJwt(userId, userEmail));

            return new AuthenticationSuccessDto
            {
                Jwt = refreshToken.Jwt,
                RefreshToken = refreshToken.Id.Value.ToString()
            };
        }
    }
}