using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using INDG.GRIP.Trader.Application.Common.Handlers;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Common.Models;
using INDG.GRIP.Trader.Application.Services;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Application.Logic.Users.Create
{
    public record CreateUserCommand : IRequest<Result<Guid>>
    {
        public string Login { get; init; }
        public string Password { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, Result<Guid>>
    {
        public CreateUserCommandHandler(ICurrentUserService currentUserService, IRepositoryManager repositoryManager,
            IMapper mapper)
            : base(currentUserService, repositoryManager, mapper)
        {
        }

        public override async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashPassword = EncoderService.GetSha256(request.Login, request.Password);

            User user = new
            (
                login: request.Login,
                password: hashPassword,
                firstName: request.FirstName,
                lastName: request.LastName
            );

            var userId = await RepositoryManager.UserRepository.AddUser(user, cancellationToken);

            return new Result<Guid>(userId);
        }
    }
}