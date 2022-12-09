using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISaleTransactionRepository
    {
        Task<IEnumerable<SaleTransaction>> GetAllPerUser(int bankAccountId, bool trackChanges);
        Task<IEnumerable<SaleTransaction>> GetAll(bool trackChanges);
        public void CreateSaleTransaction(SaleTransaction saleTransaction);
        Task<SaleTransaction> GetSale(int id, bool trackChanges);

    }
}
