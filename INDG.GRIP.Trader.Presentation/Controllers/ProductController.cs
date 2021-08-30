using System;
using System.Threading;
using System.Threading.Tasks;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Common.Models;
using INDG.GRIP.Trader.Application.Logic.Products.Create;
using INDG.GRIP.Trader.Application.Logic.Products.GetProducts;
using INDG.GRIP.Trader.Application.Logic.Products.Models;
using INDG.GRIP.Trader.Domain.Aggregates.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INDG.GRIP.Trader.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [Authorize]
    public class ProductController : BaseController
    {
        private Guid _currentUserId;
        public ProductController(ICurrentUserService currentUserService)
        {
            _currentUserId = currentUserService.GetCurrentUser().Id;
        }

        [HttpPost]
        public async Task<Result<Guid>> AddProduct(CreateProductCommand command, CancellationToken token)
        {
            return await Mediator.Send(command, token);
        }

        [HttpPut, Route("buy/{productId:guid}")]
        public async Task<Result<bool>> BuyProduct(Guid productId, CancellationToken token)
        {
            return await Mediator.Send(new SetProductSaledCommand(productId), token);
        }

        [HttpPut, Route("send/{productId:guid}/{shippingNumber}")]
        public async Task<Result<bool>> SendProduct(Guid productId, string shippingNumber, CancellationToken token)
        {
            return await Mediator.Send(new SetProductShippedCommand(productId, shippingNumber), token);
        }

        [HttpGet, Route("saled")]
        public async Task<Collection<ProductDto>> GetSaledProducts(CancellationToken token)
        {
            var query = new GetProductsQuery(Status.Saled, _currentUserId);
            return await Mediator.Send(query, token);
        }

        [HttpGet, Route("shipped")]
        public async Task<Collection<ProductDto>> GetShippedProducts(CancellationToken token)
        {
            var query = new GetProductsQuery(Status.Shipped, _currentUserId);
            return await Mediator.Send(query, token);
        }

        [HttpGet, Route("bought")]
        public async Task<Collection<ProductDto>> GetBoughtProducts(CancellationToken token)
        {
            var query = new GetProductsQuery(null, null, _currentUserId);
            return await Mediator.Send(query, token);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Collection<ProductDto>> GetProducts(CancellationToken token)
        {
            var query = new GetProductsQuery(Status.OnSale);
            return await Mediator.Send(query, token);
        }
    }
}