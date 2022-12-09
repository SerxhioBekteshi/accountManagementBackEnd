using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICompanyCategoryService
    {
        Task<bool> CreateRecord(CreateCompanyCategoryDto createCompanyCategoryDto, int userId);
        Task<bool> UpdateRecord(UpdateCompanyCategoryDto updateCompanyCategoryDto, int id, int userId);
        Task<bool> DeleteRecord(int id);
    }
}
