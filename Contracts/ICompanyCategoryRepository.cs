using Entities.DTO;
using Entities.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyCategoryRepository
    {
        void CreateRecord(CompanyCategory companyCategory);
        void DeleteRecord(CompanyCategory companyCategory);
        void UpdateRecord(CompanyCategory companyCategory);
        Task<CompanyCategory> GetRecordByIdAsync(int id);
        Task<CompanyCategory> GetCompanyByIdAsync(int id);
        Task<IEnumerable<CompanyCategory>> GetCompanyCategoryForCompanyIdAsync(int companyId);
        Task<IEnumerable<int?>> GetCategoryIdsForCompanyId(int companyId);
        Task<CompanyCategory> GetRecordByCompanyIdCategoryIdAsync(int companyId, int categoryId);

    }
}
