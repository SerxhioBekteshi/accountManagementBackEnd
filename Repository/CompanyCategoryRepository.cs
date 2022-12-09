using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyCategoryRepository : RepositoryBase<CompanyCategory>, ICompanyCategoryRepository
    {
        public CompanyCategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void CreateRecord(CompanyCategory companyCategory) => Create(companyCategory);
        public void DeleteRecord(CompanyCategory companyCategory) => Delete(companyCategory);
        public void UpdateRecord(CompanyCategory companyCategory) => Update(companyCategory);

        public async Task<CompanyCategory> GetCompanyByIdAsync(int companyId) =>
            await FindByCondition(c => c.CompanyId.Equals(companyId), true)
        .FirstOrDefaultAsync();

        public async Task<CompanyCategory> GetRecordByIdAsync(int id) =>
            await FindByCondition(c => c.Id.Equals(id))
            .SingleOrDefaultAsync();


        public async Task<IEnumerable<CompanyCategory>> GetCompanyCategoryForCompanyIdAsync(int companyId) =>
            await FindByCondition(c => c.CompanyId.Equals(companyId), true)
        .ToListAsync();


        public async Task<IEnumerable<int?>> GetCategoryIdsForCompanyId(int companyId) =>
            (IEnumerable<int?>)await FindByCondition(x => x.CompanyId.Equals(companyId))
            .Select(x => x.CategoryId)
        .ToListAsync();


        public async Task<CompanyCategory> GetRecordByCompanyIdCategoryIdAsync(int companyId, int categoryId) =>
            await FindByCondition(c => c.CompanyId.Equals(companyId) && c.CategoryId.Equals(categoryId))
            .SingleOrDefaultAsync();


    }
}
