using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync(int userId);

        Task<BankAccount> GetBankAccountAsync(int bankAccountId);

        public void UpdateBankAccount(BankAccount BankAccount);

        public void CreateBankAccount(BankAccount BankAccount);

        public void DeleteBankAccount(BankAccount BankAccount);


    }
}