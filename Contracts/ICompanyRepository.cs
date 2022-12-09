using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<int> ids);    
        Task<Company> GetCompanyAsync(int companyId);
        Task<Company> GetCompanyByManagerAsync(int managerId);
        void CreateCompany(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);
    }
}
