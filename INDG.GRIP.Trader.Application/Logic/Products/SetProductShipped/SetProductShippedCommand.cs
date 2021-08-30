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
    public class SetProductShippedCommand : IRequest<Result<bool>>
    {
        public SetProductShippedCommand(Guid productId, string shippingNumber)
        {
            ProductId = productId;
            ShippingNumber = shippingNumber;
        }

        public Guid ProductId { get; }
        public string ShippingNumber { get; }
    }

    public class SetProductShippedCommandHandler : CommandHandler<SetProductShippedCommand, Result<bool>>
    {
        public SetProductShippedCommandHandler(ICurrentUserService currentUserService, IRepositoryManager repositoryManager,
            IMapper mapper)
            : base(currentUserService, repositoryManager, mapper)
        {
        }

        public override async Task<Result<bool>> Handle(SetProductShippedCommand request, CancellationToken cancellationToken)
        {
            var product = await RepositoryManager
                .ProductRepository
                .GetProductByCondition(x => x.Id == request.ProductId, cancellationToken);

            if (product is null)
                throw new NotFoundEntityException(nameof(product), request.ProductId);

            if (!product.Status.Equals(Status.Saled) || product.SalerUserId != CurrentUser.Id)
                throw new ConflictException("Product can't be shipped");

            product.SetShipped(request.ShippingNumber);
            await RepositoryManager.SaveChangeAsync(cancellationToken);

            return new Result<bool>(true);
        }
    }
}
