using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyProductRepository : RepositoryBase<CompanyProduct>, ICompanyProductRepository
    {
        public CompanyProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void CreateRecord(CompanyProduct companyProduct) => Create(companyProduct);
        public void DeleteRecord(CompanyProduct companyProduct) => Delete(companyProduct);
        public void UpdateRecord(CompanyProduct companyProduct) => Update(companyProduct);

        public async Task<CompanyProduct> GetCompanyByIdAsync(int companyId) =>
            await FindByCondition(c => c.CompanyId.Equals(companyId), true)
        .FirstOrDefaultAsync();

        public async Task<CompanyProduct> GetRecordByIdAsync(int id) =>
            await FindByCondition(c => c.Id.Equals(id))
            .SingleOrDefaultAsync();


        public async Task<IEnumerable<CompanyProduct>> GetCompanyProductForCompanyIdAsync(int companyId) =>
            await FindByCondition(c => c.CompanyId.Equals(companyId), true)
        .ToListAsync();


        public async Task<IEnumerable<int?>> GetProductIdsForCompanyId(int companyId) =>
            (IEnumerable<int?>)await FindByCondition(x => x.CompanyId.Equals(companyId))
            .Select(x => x.ProductId)
        .ToListAsync();


        public async Task<CompanyProduct> GetRecordByCompanyIdProductIdAsync(int companyId, int productId) =>
            await FindByCondition(c => c.CompanyId.Equals(companyId) && c.ProductId.Equals(productId))
            .SingleOrDefaultAsync();
    }
}
