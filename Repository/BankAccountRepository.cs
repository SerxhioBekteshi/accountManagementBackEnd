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
    public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBankAccount(BankAccount BankAccount)
        {
            Create(BankAccount);
        }
        public async Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync(int userId) => 
            FindByCondition(c => c.ClientId.Equals(userId)).OrderBy(c => c.Name).ToList();

        public async Task<BankAccount> GetBankAccountAsync(int bankAccountId) => 
            FindByCondition(c => c.Id == bankAccountId).SingleOrDefault();

        public void UpdateBankAccount(BankAccount BankAccount)
        {
            Update(BankAccount);
        }

        public void DeleteBankAccount(BankAccount BankAccount)
        {
            Delete(BankAccount);
        }
    }
}