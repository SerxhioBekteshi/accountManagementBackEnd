using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CurrencyBankRepository : RepositoryBase<CurrencyBank>, ICurrencyBankAccountRepository
    {

        public CurrencyBankRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void CreateRecord(CurrencyBank companyCategory) => Create(companyCategory);
        public void DeleteRecord(CurrencyBank companyCategory) => Delete(companyCategory);
        public void UpdateRecord(CurrencyBank companyCategory) => Update(companyCategory);

        public async Task<CurrencyBank> GetBankByIdAsync(int companyId) =>
            await FindByCondition(c => c.BankId.Equals(companyId), true)
        .FirstOrDefaultAsync();

        public async Task<CurrencyBank> GetRecordByIdAsync(int id) =>
            await FindByCondition(c => c.Id.Equals(id))
            .SingleOrDefaultAsync();
        public async Task<CurrencyBank> GetCurrencyByIdAsync(int currencyId) =>
            await FindByCondition(c => c.CurrencyId.Equals(currencyId), true)
        .FirstOrDefaultAsync();
        public async Task<IEnumerable<CurrencyBank>> GetCurrencyBankAccountForBankAccountIdAsync(int bankId) =>
            await FindByCondition(c => c.BankId.Equals(bankId), true)
        .ToListAsync();

        public async Task<IEnumerable<CurrencyBank>> GetCurrencyBankAccountForCurrencyIdAsync(int currencyId) =>
            await FindByCondition(c => c.CurrencyId.Equals(currencyId), true)
        .ToListAsync();

        public async Task<IEnumerable<int?>> GetCurrenciesIdsForBankAccountId(int bankId) =>
            (IEnumerable<int?>)await FindByCondition(x => x.BankId.Equals(bankId))
            .Select(x => x.CurrencyId)
        .ToListAsync();
        public async Task<IEnumerable<int?>> GetBankIdsForCurrencyId(int currencyId) =>
            (IEnumerable<int?>)await FindByCondition(x => x.CurrencyId.Equals(currencyId))
            .Select(x => x.CurrencyId)
        .ToListAsync();

        public async Task<CurrencyBank> GetRecordByCurrencyIdBankIdAsync(int currencyId, int bankId) =>
        await FindByCondition(c => c.CurrencyId.Equals(currencyId) && c.BankId.Equals(bankId))
        .SingleOrDefaultAsync();

    }
}
