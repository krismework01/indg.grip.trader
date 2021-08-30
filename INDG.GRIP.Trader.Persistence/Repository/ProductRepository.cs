using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Domain.Aggregates.Users;
using System.Linq.Expressions;
using INDG.GRIP.Trader.Domain.Aggregates.Products;
using System.Linq;

namespace INDG.GRIP.Trader.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> AddProduct(Product product, CancellationToken token = default)
        {
            var data = await _context.Products.AddAsync(product, token);
            await _context.SaveChangesAsync(token);
            return data.Entity.Id;
        }

        public async Task<Product[]> GetProductsByCondition(Expression<Func<Product, bool>> predicate, CancellationToken token = default)
        {
            return await _context.Products.Where(predicate).ToArrayAsync(token);
        }

        public async Task<Product> GetProductByCondition(Expression<Func<Product, bool>> predicate, CancellationToken token = default)
        {
            return await _context.Products.Include(x => x.Status).SingleOrDefaultAsync(predicate, token);
        }
    }
}