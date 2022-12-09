using Entities.DTO;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<PagedListResponse<IEnumerable<CompanyDto>>> GetAllRecords(RequestTableBodyDto request); //PAGINATION FOR TABLE COMPANY FOR ADMIN 
        Task<PagedListResponse<IEnumerable<CompanyDto>>> GetAllCompaniesPerManager(int userId, RequestTableBodyDto request);
        Task<int> CreateRecord(CompanyForCreationAndUpdateDto createCompanyDTO, int userId); //ADMIN CREATES A COMPANY 
        Task<bool> UpdateRecord(int id, CompanyForCreationAndUpdateDto updateCompanyDto, int userId);
        Task<bool> DeleteRecord(int id);
        Task<CompanyDto> GetRecordById(int id);
        Task<IEnumerable<CategoryDto>> GetCategoriesForCompany(int companyId, int userId);
        Task<bool> PostOtherCategoriesForCompany(int companyId, PostCategoriesToCompanyDto postCategoriesToCompanyId, int userId);
        //Task<IEnumerable<CategoriesForCompanyListDto>> GetAllRecordsByCompanyId(int companyId, int managerId);
        //Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(int managerId);
        Task<bool> DeleteRelationCategory(int companyId, int categoryId);
        Task<bool> DeleteRelationProduct(int companyId, int productId);
        public Task<bool> AssignManagerForCompany(int companyId, CompanyManagerDto manager);

    }
}
