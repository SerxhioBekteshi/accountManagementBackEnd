using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Repository
{
    public class BankAccountRepository: RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBankAccount(BankAccount bankAccount)
        {
            Create(bankAccount);
        }
        public async Task<IEnumerable<BankAccount>> GetAllBankAccountssAsync(int userId) =>
            FindAll();

        public async Task<BankAccount> GetBankAccountAsync(int bankAccountId) =>
            FindByCondition(c => c.Id == bankAccountId).SingleOrDefault();

        public void UpdateBankAccount(BankAccount bankAccount)
        {
            Update(bankAccount);
        }

        public void DeleteBankAccount(BankAccount bankAccount)
        {
            Delete(bankAccount);
        }
    }
}
