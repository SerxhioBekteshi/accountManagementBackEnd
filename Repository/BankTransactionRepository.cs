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
    public class BankTransactionRepository : RepositoryBase<BankTransaction>, IBankTransactionRepository
    {
        public BankTransactionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<BankTransaction>> GetAllBankAccountTransactionsAsync(bool trackChanges) =>
        FindAll(trackChanges).OrderBy(c => c.Amount).ToList();
        public void CreateBankTransaction(BankTransaction bankTransaction)
        {
            Create(bankTransaction);
        }

    }
}