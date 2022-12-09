using AutoMapper;
using Contracts;
using Entities.DTC;
using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using SendGrid.Helpers.Errors.Model;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Service
{
    public class CompanyService: ICompanyService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapperRepository;

        public CompanyService(ILoggerManager logger,
            IMapper mapper,
            IRepositoryManager repositoryManager,
            IDapperRepository dapperRepository
            )
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _dapperRepository = dapperRepository;

        }

        public async Task<PagedListResponse<IEnumerable<CompanyDto>>> GetAllRecords(RequestTableBodyDto request)
        {
            try
            {
                var companyWithMetaData = await _dapperRepository.SearchCompanies(request);
                var companyDtoList = _mapper.Map<IEnumerable<CompanyDto>>(companyWithMetaData);
                var columns = GetDataTableColumns();

                PagedListResponse<IEnumerable<CompanyDto>> response = new PagedListResponse<IEnumerable<CompanyDto>>
                {
                    TotalCount = companyWithMetaData.MetaData.TotalCount,
                    CurrentPage = companyWithMetaData.MetaData.CurrentPage,
                    PageSize = companyWithMetaData.MetaData.PageSize,
                    Columns = columns,
                    Rows = companyDtoList,
                    Key = companyWithMetaData.MetaData.Key,
                    hasNext = companyWithMetaData.MetaData.hasNext,
                    hasPrevious = companyWithMetaData.MetaData.hasPrevious,

                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetAllRecords), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<PagedListResponse<IEnumerable<CompanyDto>>> GetAllCompaniesPerManager(int userId, RequestTableBodyDto request)
        {
            try
            {
                var companyWithMetaData = await _dapperRepository.SearchCompaniesByLoggedInManager(userId, request);
                var companyDtoList = _mapper.Map<IEnumerable<CompanyDto>>(companyWithMetaData);
                var columns = GetDataTableColumns();

                PagedListResponse<IEnumerable<CompanyDto>> response = new PagedListResponse<IEnumerable<CompanyDto>>
                {
                    TotalCount = companyWithMetaData.MetaData.TotalCount,
                    CurrentPage = companyWithMetaData.MetaData.CurrentPage,
                    PageSize = companyWithMetaData.MetaData.PageSize,
                    Columns = columns,
                    Rows = companyDtoList,
                    Key = companyWithMetaData.MetaData.Key,
                    hasNext = companyWithMetaData.MetaData.hasNext,
                    hasPrevious = companyWithMetaData.MetaData.hasPrevious,

                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetAllRecords), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<int> CreateRecord(CompanyForCreationAndUpdateDto createCompanyDTO, int userId)
        {
            try
            {
                var company = _mapper.Map<Company>(createCompanyDTO);

                company.DateCreated = DateTime.UtcNow;
                company.CreatedBy = userId;
                company.ManagerAccountActivated = "Not Registered";

                _repositoryManager.Company.CreateCompany(company);
                await _repositoryManager.SaveAsync();

               
                CreateCompanyCategoryRelation(createCompanyDTO.CategoryIds, company.Id, userId);
                await _repositoryManager.SaveAsync();
                
                   
                return company.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(CreateRecord), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> DeleteRecord(int id)
        {
            try
            {
                
                var existingCompany = await GetCompanyIdAsync(id);

                if(existingCompany != null)
                {
                    await DeleteCompanyCategoryRelationForCompanyId(id);
                    _repositoryManager.Company.DeleteCompany(existingCompany);
                }

                await _repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRecord), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> DeleteRelationCategory(int companyId, int categoryId)
        {
            try
            {
                var existingRelation = await _repositoryManager.CompanyCategory.GetRecordByCompanyIdCategoryIdAsync(companyId, categoryId);
                if (existingRelation is null)
                    throw new NotFoundException(string.Format("There is no relation between company with Id: {0} and category with Id: {1}!", companyId, categoryId));

                _repositoryManager.CompanyCategory.DeleteRecord(existingRelation);


                await _repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRelationCategory), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> DeleteRelationProduct(int companyId, int productId)
        {
            try
            {

                var existingRelation = await _repositoryManager.CompanyProduct.GetRecordByCompanyIdProductIdAsync(companyId, productId);
                if (existingRelation is null)
                    throw new NotFoundException(string.Format("There is no relation between company with Id: {0} and product with Id: {1}!", companyId, productId));

                _repositoryManager.CompanyProduct.DeleteRecord(existingRelation);

                await _repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRelationProduct), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> AssignManagerForCompany(int companyId, CompanyManagerDto manager)
        {
            try
            {

                var existingCompany = await _dapperRepository.GetCompanyById(companyId);
                if (existingCompany is null)
                    throw new NotFoundException(string.Format("Company with Id: {0}! doesn't exist", companyId));

                var existingManager = await _repositoryManager.User.GetUserDetails(trackChanges: false, manager.managerId.ToString() );
                if (existingManager is null)
                {
                    throw new NotFoundException(string.Format("User with id: {0} not found!", manager.managerId));
                }

                var companyEntity = _mapper.Map<Company>(existingCompany);

                companyEntity.ManagerAccountActivated = "Registered";
                companyEntity.ManagerId = manager.managerId;
                companyEntity.CreatedBy = 3;
                companyEntity.DateCreated = DateTime.Now;
                _repositoryManager.Company.UpdateCompany(companyEntity);
                _repositoryManager.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(AssignManagerForCompany), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        //public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(int managerId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<IEnumerable<CategoriesForCompanyListDto>> GetAllRecordsByCompanyId(int companyId, int managerId)
        //{
        //    throw new NotImplementedException();
        //}

        private List<DataTableColumn> GetDataTableColumns()
        {
            var columns = GenerateDataTableColumn<CompanyColumn>.GetDataTableColumns();
            return columns;
        }
        public async Task<CompanyDto> GetRecordById(int id)
        {

            try
            {
                var existingCompany = await _dapperRepository.GetCompanyById(id);
                if (existingCompany is null)
                    throw new NotFoundException(string.Format("Company with Id: {0} was not found!", id));

                var categoriesIdForCompany = await _repositoryManager.CompanyCategory.GetCategoryIdsForCompanyId(id);
                if (categoriesIdForCompany.Any())
                    existingCompany.CategoryIds = categoriesIdForCompany.ToList();

                return existingCompany;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetRecordById), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> PostOtherCategoriesForCompany(int companyId, PostCategoriesToCompanyDto postCategoriesToCompanyId, int managerId)
        {
            try
            {
                var existingCompany = await GetCompanyIdAsync(companyId);

                if (existingCompany is null)
                    throw new NotFoundException(string.Format("Company with id: {0} not found!", companyId));

                foreach(var categoryId in postCategoriesToCompanyId.CategoryIds)
                {
                    var existingCategory = await _repositoryManager.Category.GetCategory((int)categoryId);
                    if (existingCategory is null)
                    {
                        throw new NotFoundException(string.Format("Category with id: {0} not found! Please contact administrator!", categoryId));
                    }
                }

                _mapper.Map(postCategoriesToCompanyId, existingCompany);

                var companyCategoryIds = await _repositoryManager.CompanyCategory.GetCategoryIdsForCompanyId(companyId);
                var categoriesToAdd = postCategoriesToCompanyId?.CategoryIds?.Where(x => !companyCategoryIds.Contains(x)).ToList();

                if (categoriesToAdd.Count == 0)
                {
                    await DeleteCompanyCategoryRelationForCompanyId(companyId);
                    var categoriesToAdd2 = postCategoriesToCompanyId?.CategoryIds.ToList();
                    CreateCompanyCategoryRelation(categoriesToAdd2, companyId, managerId);
                }
                else
                    CreateCompanyCategoryRelation(categoriesToAdd, companyId, managerId);


                await _repositoryManager.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(PostOtherCategoriesForCompany), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesForCompany (int companyId, int userId)
        {
            try
            {
                var existingCompany = await GetCompanyIdAsync(companyId);

                if (existingCompany is null)
                    throw new NotFoundException(string.Format("Company with id: {0} not found!", companyId));

                var companyCategories = await _dapperRepository.GetCategoriesForCompanyId(companyId);
           
                return companyCategories;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetCategoriesForCompany), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(int id, CompanyForCreationAndUpdateDto updateCompanyDto, int userId)
        {
            var existingCompany = await GetCompanyIdAsync(id);

            var existingCategoryCompany = existingCompany.CompanyCategory;

            //var existingCategoryProduct = existingCompany.CompanyProduct;

            await UpdateCompany(existingCompany, updateCompanyDto, userId);

            await UpdateCompanyCategories(existingCategoryCompany, updateCompanyDto, id, userId);

            //await UpdateCompanyProducts(existingCategoryProduct, updateCompanyDto, id, userId);


            return true;
        }

        #region Private Methods
        private async Task<Company> GetCompanyIdAsync(int id)
        {
            var existingCompany = await _repositoryManager.Company.GetCompanyAsync(id);
            return existingCompany;
        }

        private async Task UpdateCompany(Company existingCompany, CompanyForCreationAndUpdateDto updateCompanyDto, int userId)
        {
            try
            {
                _mapper.Map(updateCompanyDto, existingCompany);

                existingCompany.DateModified = DateTime.UtcNow;
                existingCompany.ModifiedBy = userId;
                _repositoryManager.Company.UpdateCompany(existingCompany);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateCompany), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }


        private void CreateCompanyCategoryRelation(List<int?>? categoryIds, int companyId, int userId)
        {
            if (categoryIds is not null)
            {
                foreach (var selectedCategory in categoryIds)
                {
                    var newCompanyCategory= new CompanyCategory
                    {
                        CategoryId = (int)selectedCategory,
                        CompanyId = companyId,
                        DateCreated = DateTime.UtcNow,
                        CreatedBy = userId
                    };
                    _repositoryManager.CompanyCategory.CreateRecord(newCompanyCategory);
                }
            }
        }

        private void CreateCompanyProductRelation(List<int?>? productIds, int companyId, int userId)
        {
            if (productIds is not null)
            {
                foreach (var selectedCategory in productIds)
                {
                    var newCompanyProduct= new CompanyProduct
                    {
                        ProductId = (int)selectedCategory,
                        CompanyId = companyId,
                        DateCreated = DateTime.UtcNow,
                        CreatedBy = userId,
                    };
                    _repositoryManager.CompanyProduct.CreateRecord(newCompanyProduct);
                }
            }
        }

        private async Task DeleteCompanyCategoryRelationForCompanyId(int companyId)
        {
            var existingCompanyCategory = await _repositoryManager.CompanyCategory.GetCompanyCategoryForCompanyIdAsync(companyId);
            foreach (var existingCategory in existingCompanyCategory)
            {
                _repositoryManager.CompanyCategory.DeleteRecord(existingCategory);
            }
        }

        private async Task DeleteCompanyProductRelationForCompanyId(int companyId)
        {
            var existingCompanyProduct = await _repositoryManager.CompanyProduct.GetCompanyProductForCompanyIdAsync(companyId);
            foreach (var existingProduct in existingCompanyProduct)
            {
                _repositoryManager.CompanyProduct.DeleteRecord(existingProduct);
            }
        }

        private async Task UpdateCompanyCategories(List<CompanyCategory> existingCompanyCategory, CompanyForCreationAndUpdateDto updateCompanyDto, int companyId, int userId)
        {
            try
            {
                
                if (existingCompanyCategory != null && existingCompanyCategory.Count() > 0 && updateCompanyDto.CategoryIds is not null)
                {
                    var ids = existingCompanyCategory.Select(x => x.CategoryId).ToList();
                    var check = ids.Except(updateCompanyDto.CategoryIds);

                    if (check.Count() != 0)
                    {
                        await DeleteCompanyCategoryRelationForCompanyId(companyId);
                        CreateCompanyCategoryRelation(updateCompanyDto.CategoryIds, companyId, userId);
                    }
                }
                else
                {
                    if (updateCompanyDto.CategoryIds is not null)
                        CreateCompanyCategoryRelation(updateCompanyDto.CategoryIds, companyId, userId);
                }

                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)  
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateCompanyCategories), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }


        //private async Task UpdateCompanyProducts(List<CompanyProduct> existingCompanyProduct, CompanyForCreationAndUpdateDto updateCompanyDto, int companyId, int userId)
        //{
        //    try
        //    {
        //        if (existingCompanyProduct != null && existingCompanyProduct.Count() > 0 && updateCompanyDto.CategoryIds is not null)
        //        {
        //            var ids = existingCompanyProduct.Select(x => x.ProductId).ToList();
        //            var check = ids.Except(updateCompanyDto.ProductIds);

        //            if (check.Count() != 0)
        //            {
        //                await DeleteCompanyProductRelationForCompanyId(companyId);
        //                CreateCompanyProductRelation(updateCompanyDto.ProductIds, companyId, userId);
        //            }
        //        }
        //        else
        //        {
        //            if (updateCompanyDto.ProductIds is not null)
        //                CreateCompanyCategoryRelation(updateCompanyDto.ProductIds, companyId, userId);
        //        }

        //        await _repositoryManager.SaveAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(string.Format("{0}: {1}", nameof(UpdateCompanyProducts), ex.Message));
        //        throw new BadRequestException(ex.Message);
        //    }
        //}
        #endregion
    }
}
