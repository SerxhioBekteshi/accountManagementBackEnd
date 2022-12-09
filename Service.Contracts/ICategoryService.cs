using Entities.DTO;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        Task<int> CreateRecord(CategoryForCreationAndUpdateDto createCategoryDTO, int userId);
        Task<CategoryDto> GetRecordById(int id);
        Task<bool> UpdateRecord(int id, CategoryForCreationAndUpdateDto updateCategoryDto, int userId);
        Task<bool> DeleteRecord(int id);
        Task<IEnumerable<CategoryListDto>> GetCategoriesAsAList();
        Task<PagedListResponse<IEnumerable<CategoryDto>>> GetAllRecords(RequestTableBodyDto filter);
    }
}
