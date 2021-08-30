using System.Threading;
using System.Threading.Tasks;
using MediatR;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using AutoMapper;

namespace INDG.GRIP.Trader.Application.Common.Handlers
{
    public abstract class CommandHandler<TQ, TM> : IRequestHandler<TQ, TM> where TQ : IRequest<TM>
    {
        protected ICurrentUser CurrentUser { get; }
        protected IRepositoryManager RepositoryManager { get; }
        protected IMapper Mapper { get; }

        protected CommandHandler(ICurrentUserService currentUserService, IRepositoryManager repositoryManager, IMapper mapper)
        {
            CurrentUser = currentUserService.GetCurrentUser();
            RepositoryManager = repositoryManager;
            Mapper = mapper;
        }
        public abstract Task<TM> Handle(TQ request, CancellationToken cancellationToken);
    }
}