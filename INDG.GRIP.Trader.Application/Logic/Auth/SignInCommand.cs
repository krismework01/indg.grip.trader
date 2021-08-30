using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using INDG.GRIP.Trader.Application.Common.Exceptions;
using INDG.GRIP.Trader.Application.Common.Handlers;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Services;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Application.Logic.Auth
{
    public record SignInCommand : IRequest<AuthResponse>
    {
        public string Login { get; init; }
        public string Password { get; init; }
    }

    public class SignInCommandHandler : CommandHandler<SignInCommand, AuthResponse>
    {
        private readonly IJwtManager _jwtManager;
        private readonly IIdentityService _identityService;

        public SignInCommandHandler(ICurrentUserService currentUserService, IRepositoryManager repositoryManager,
            IJwtManager jwtManager,
            IIdentityService identityService, IMapper mapper)
            : base(currentUserService, repositoryManager, mapper)
        {
            _jwtManager = jwtManager;
            _identityService = identityService;
        }

        public override async Task<AuthResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await GetUserByLoginAndPassword(request.Login, request.Password, cancellationToken);

            if (user is null)
                throw new AuthException("Auth error");

            var identityMaster = _identityService.BuildIdentityMaster(user);
            var session = _jwtManager.GenerationToken(identityMaster.Identity, identityMaster.user);

            return new AuthResponse { Token = session.Token };
        }

        private async Task<User> GetUserByLoginAndPassword(string login, string password, CancellationToken token)
        {
            var user = await RepositoryManager
                .UserRepository
                .GetUserByCondition(x => x.Login == login, token);

            var hashPassword = EncoderService.GetSha256(login, password);

            if (user.Password != hashPassword)
                return null;

            return user;
        }
    }
}