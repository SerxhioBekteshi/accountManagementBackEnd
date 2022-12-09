using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICompanyProductService
    {
        Task<bool> CreateRecord(CompanyProductCreateUpdateDto createCompanyCategoryDto, int userId);
        Task<bool> UpdateRecord(CompanyProductCreateUpdateDto updateCompanyCategoryDto, int id, int userId);
        Task<bool> DeleteRecord(int id);
    }
}
