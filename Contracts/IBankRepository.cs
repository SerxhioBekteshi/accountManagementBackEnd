using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBankRepository
    {
        Task<IEnumerable<Bank>> GetAllBanksAsync(int userId);

        Task<Bank> GetBankAsync(int bankId);

        public void UpdateBank(Bank bank);

        public void CreateBank(Bank bank);

        public void DeleteBank(Bank bank);


    }
}