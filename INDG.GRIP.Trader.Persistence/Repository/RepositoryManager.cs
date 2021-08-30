using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Domain.Aggregates.Products;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Persistence.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IDbContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IProductRepository> _productRepository;

        public RepositoryManager(IDbContext context, IMapper mapper)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context, mapper));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context, mapper));
        }

        public IUserRepository UserRepository => _userRepository.Value;
        public IProductRepository ProductRepository => _productRepository.Value;
        public async Task<int> SaveChangeAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }
    }
}