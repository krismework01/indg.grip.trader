using AutoMapper;
using INDG.GRIP.Trader.Application.Common.Handlers;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Common.Models;
using INDG.GRIP.Trader.Application.Logic.Products.Models;
using INDG.GRIP.Trader.Domain.Aggregates.Products;
using INDG.GRIP.Trader.Domain.Common.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace INDG.GRIP.Trader.Application.Logic.Products.GetProducts
{
    public class SetProductSaledCommand : IRequest<Result<bool>>
    {
        public SetProductSaledCommand(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; }
    }

    public class SetProductSaledCommandHandler : CommandHandler<SetProductSaledCommand, Result<bool>>
    {
        public SetProductSaledCommandHandler(ICurrentUserService currentUserService, IRepositoryManager repositoryManager,
            IMapper mapper)
            : base(currentUserService, repositoryManager, mapper)
        {
        }

        public override async Task<Result<bool>> Handle(SetProductSaledCommand request, CancellationToken cancellationToken)
        {
            var product = await RepositoryManager
                .ProductRepository
                .GetProductByCondition(x => x.Id == request.ProductId, cancellationToken);

            if (product is null)
                throw new NotFoundEntityException(nameof(product), request.ProductId);

            if (!product.Status.Equals(Status.OnSale) || product.SalerUserId == CurrentUser.Id)
                throw new ConflictException("Product can't be saled");

            product.SetSaled(CurrentUser.Id);
            await RepositoryManager.SaveChangeAsync(cancellationToken);

            return new Result<bool>(true);
        }
    }
}
