using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICurrencyBankAccountRepository
    {
        public void CreateRecord(CurrencyBank companyCategory);
        public void DeleteRecord(CurrencyBank companyCategory);
        public void UpdateRecord(CurrencyBank companyCategory);
        public Task<CurrencyBank> GetBankByIdAsync(int currencyId);
        public Task<CurrencyBank> GetCurrencyByIdAsync(int bankAccountId);
        public Task<CurrencyBank> GetRecordByIdAsync(int id);
        public Task<IEnumerable<CurrencyBank>> GetCurrencyBankAccountForBankAccountIdAsync(int bankAccountid);
        public Task<IEnumerable<int?>> GetCurrenciesIdsForBankAccountId(int bankAccountid);
    }
}
