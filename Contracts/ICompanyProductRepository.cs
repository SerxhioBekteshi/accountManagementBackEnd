using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyProductRepository
    {
        public void CreateRecord(CompanyProduct companyCategory);
        public void DeleteRecord(CompanyProduct companyCategory);
        public void UpdateRecord(CompanyProduct companyCategory);
        public Task<CompanyProduct> GetCompanyByIdAsync(int companyId);
        public Task<CompanyProduct> GetRecordByIdAsync(int id);
        public Task<IEnumerable<CompanyProduct>> GetCompanyProductForCompanyIdAsync(int companyId);
        public Task<IEnumerable<int?>> GetProductIdsForCompanyId(int companyId);
        public Task<CompanyProduct> GetRecordByCompanyIdProductIdAsync(int companyId, int productId);
    }
}
