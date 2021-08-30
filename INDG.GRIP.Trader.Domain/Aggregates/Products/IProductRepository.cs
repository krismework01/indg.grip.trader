using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace INDG.GRIP.Trader.Domain.Aggregates.Products
{
    public interface IProductRepository
    {
        Task<Guid> AddProduct(Product product, CancellationToken token = default);
        Task<Product[]> GetProductsByCondition(Expression<Func<Product, bool>> predicate, CancellationToken token = default);
        Task<Product> GetProductByCondition(Expression<Func<Product, bool>> predicate, CancellationToken token = default);
    }
}
