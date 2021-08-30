using AutoMapper;
using INDG.GRIP.Trader.Application.Common.Handlers;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Common.Models;
using INDG.GRIP.Trader.Application.Logic.Products.Models;
using INDG.GRIP.Trader.Domain.Aggregates.Products;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace INDG.GRIP.Trader.Application.Logic.Products.GetProducts
{
    public class GetProductsQuery : IRequest<Collection<ProductDto>>
    {
        public GetProductsQuery(Status status, Guid? salerUserId = null, Guid? buyerUserId = null)
        {
            Status = status;
            SalerUserId = salerUserId;
            BuyerUserId = buyerUserId;
        }

        public Status Status { get; }
        public Guid? SalerUserId { get; }
        public Guid? BuyerUserId { get; }
    }

    public class GetProductsQueryHandler : CommandHandler<GetProductsQuery, Collection<ProductDto>>
    {
        public GetProductsQueryHandler(ICurrentUserService currentUserService, IRepositoryManager repositoryManager,
            IMapper mapper)
            : base(currentUserService, repositoryManager, mapper)
        {
        }

        public override async Task<Collection<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await RepositoryManager
                .ProductRepository
                .GetProductsByCondition(x => 
                    (request.Status == null || x.Status.Equals(request.Status))
                    && (request.SalerUserId == null || x.SalerUserId == request.SalerUserId.Value)
                    && (request.BuyerUserId == null || x.BuyerUserId == request.BuyerUserId.Value)
                , cancellationToken);

            var dto = Mapper.Map<Product[], ProductDto[]>(products);
            return new Collection<ProductDto>(dto);
        }
    }
}
