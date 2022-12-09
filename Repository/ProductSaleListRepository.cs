

using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductSaleListRepository : RepositoryBase<ProductSaleList>, IProductSaleListRepository
    {
        public ProductSaleListRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ProductSaleList>> GetProductSaleListPerSale(int id, bool trackChanges) => FindByCondition( c=> c.SaleTransactionId == id , trackChanges).ToList();
    }
}
