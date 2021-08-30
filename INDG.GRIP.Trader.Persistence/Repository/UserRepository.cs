using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Domain.Aggregates.Users;
using System.Linq.Expressions;

namespace INDG.GRIP.Trader.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> AddUser(User user, CancellationToken token = default)
        {
            var data = await _context.Users.AddAsync(user, token);
            await _context.SaveChangesAsync(token);
            return data.Entity.Id;
        }

        public Task<User> GetUserByCondition(Expression<Func<User, bool>> predicate, CancellationToken token = default)
        {
            return _context.Users.SingleOrDefaultAsync(predicate, token);
        }
    }
}