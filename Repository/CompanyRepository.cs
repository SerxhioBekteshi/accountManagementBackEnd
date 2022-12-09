using Contracts;
using Dapper;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync() =>
            FindAll().OrderBy(c => c.Name).ToList();

        public async Task <IEnumerable<Company>> GetByIdsAsync(IEnumerable<int> ids) =>
            FindByCondition(x => ids.Contains(x.Id)).ToList();

        public async Task<Company> GetCompanyAsync(int companyId) =>
            await FindByCondition(c => c.Id.Equals(companyId))
            //.Include(x => x.CompanyCategory)
            .FirstOrDefaultAsync();
        
        public async Task<Company> GetCompanyByManagerAsync(int managerId) =>
           FindByCondition(c => c.ManagerId == managerId)
           .SingleOrDefault();
        public void CreateCompany(Company company) => Create(company);
        public void UpdateCompany(Company company) => Update(company);
        public void DeleteCompany(Company company) 
        {
            Delete(company);
        }
    }
}
