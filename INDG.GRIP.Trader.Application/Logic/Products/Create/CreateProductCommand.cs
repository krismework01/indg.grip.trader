using AutoMapper;
using INDG.GRIP.Trader.Application.Common.Handlers;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Common.Models;
using INDG.GRIP.Trader.Domain.Aggregates.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace INDG.GRIP.Trader.Application.Logic.Products.Create
{
    public class CreateProductCommand : IRequest<Result<Guid>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public int Price { get; init; }
    }

    public class CreateProductCommanddHandler : CommandHandler<CreateProductCommand, Result<Guid>>
    {
        public CreateProductCommanddHandler(ICurrentUserService currentUserService, IRepositoryManager repositoryManager,
            IMapper mapper)
            : base(currentUserService, repositoryManager, mapper)
        {
        }

        public override async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var status = Status.From(Status.OnSale.Id);
            try
            {
                Product product = new
                (
                    name: request.Name,
                    description: request.Description,
                    price: request.Price,
                    salerUserId: CurrentUser.Id,
                    status
                );

                var productId = await RepositoryManager.ProductRepository.AddProduct(product, cancellationToken);
                return new Result<Guid>(productId);
            }
            catch(Exception ex)
            {
                var s = 0;
            }

            return null;
        }
    }
}
