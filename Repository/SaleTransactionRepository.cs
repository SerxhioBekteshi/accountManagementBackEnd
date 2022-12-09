using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SaleTransactionRepository : RepositoryBase<SaleTransaction>, ISaleTransactionRepository
    {
        public SaleTransactionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void CreateSaleTransaction(SaleTransaction saleTransaction)
        {
            Create(saleTransaction);
        }
        public async Task<IEnumerable<SaleTransaction>> GetAllPerUser(int bankAccountId, bool trackChanges) => 
            FindByCondition( c => c.BankAccountId == bankAccountId, trackChanges).ToList();
        public async Task<IEnumerable<SaleTransaction>> GetAll(bool trackChanges) =>
            FindAll(trackChanges).ToList();
        public async Task<SaleTransaction> GetSale(int id, bool trackChanges) => FindByCondition( c=> c.Id == id, trackChanges).SingleOrDefault();

    }
}
