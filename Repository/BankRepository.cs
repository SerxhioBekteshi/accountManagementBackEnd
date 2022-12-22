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
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBank(Bank bank)
        {
            Create(bank);
        }
        public async Task<IEnumerable<Bank>> GetAllBanksAsync(int userId) => 
            FindAll();

        public async Task<Bank> GetBankAsync(int bankId) => 
            FindByCondition(c => c.Id == bankId).SingleOrDefault();

        public void UpdateBank(Bank bank)
        {
            Update(bank);
        }

        public void DeleteBank(Bank bank)
        {
            Delete(bank);
        }
    }
}