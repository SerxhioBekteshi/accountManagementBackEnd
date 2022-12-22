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
        Task<IEnumerable<BankAccount>> GetAllBankAccountssAsync(int userId);

        Task<BankAccount> GetBankAccountAsync(int bankAccountId);

        public void UpdateBankAccount(BankAccount bankAccount);

        public void CreateBankAccount(BankAccount bankAccount);

        public void DeleteBankAccount(BankAccount bankAccount);

    }
}
