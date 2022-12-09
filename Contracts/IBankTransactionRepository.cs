using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBankTransactionRepository
    {
        Task<IEnumerable<BankTransaction>> GetAllBankAccountTransactionsAsync(bool trackChanges);
        public void CreateBankTransaction(BankTransaction bankTransaction);
    }
}