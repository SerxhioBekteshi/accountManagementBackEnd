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
    public interface IProductService
    {
        Task<PagedListResponse<IEnumerable<ProductDto>>> GetAllRecords(RequestTableBodyDto request);
        Task<int> CreateRecord(ProductForCreationAndUpdateDto createProductDTO, int userId);
        Task<bool> UpdateRecord(int id, ProductForCreationAndUpdateDto updateProductDto, int userId);
        Task<bool> DeleteRecord(int id);
        Task<ProductDto> GetRecordById(int id);
    }
}
