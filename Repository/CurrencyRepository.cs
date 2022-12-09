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
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCurrency(Currency currency)
        {
            Create(currency);
        }

        public void DeleteCurrency(Currency currency)
        {
            Delete(currency);
        }

        public void UpdateCurrency(Currency currency)
        {
            Update(currency);
        }

        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync(bool trackChanges) => 
            FindAll(trackChanges).OrderBy(c => c.Code).ToList();

        public async Task<Currency> GetCurrencyAsync(int currencyId, bool trackChanges) => 
            FindByCondition(c => c.Id == currencyId, trackChanges).SingleOrDefault();
    }
}
