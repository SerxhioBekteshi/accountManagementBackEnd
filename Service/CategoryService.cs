using AutoMapper;
using Contracts;
using Entities.DTC;
using Entities.DTO;
using Entities.Models;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System;
using Contracts;
using AutoMapper;
using Entities.ResponseFeatures;
using Entities.DTC;
using Service.Contracts;
using SendGrid.Helpers.Errors.Model;
using Entities.DTO;
using Entities.RequestFeatures;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapperRepository;
        public CategoryService(ILoggerManager logger,
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

        public async Task<int> CreateRecord(CategoryForCreationAndUpdateDto createCategoryDto, int userId)
        {
            try
            {
                var category = _mapper.Map<Category>(createCategoryDto);

                category.DateCreated = DateTime.UtcNow;
                category.CreatedBy = userId;

                _repositoryManager.Category.CreateCategory(category);
                await _repositoryManager.SaveAsync();

                return category.Id;
               
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
                var existingCategory = await GetCategoryByIdAsync(id);
                if (existingCategory != null)
                {
                    _repositoryManager.Category.DeleteCategory(existingCategory);
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

        public async Task<CategoryDto> GetRecordById(int id)
        {
            try
            {
                var existingCategory = await _dapperRepository.GetCategoryById(id);
                if (existingCategory is null)
                    throw new NotFoundException(string.Format("Category with Id: {0} was not found!", id));

                return existingCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetRecordById), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(int id, CategoryForCreationAndUpdateDto updateCategoryDto, int userId)
        {
            var existingCategory = await GetCategoryByIdAsync(id);

            await UpdateCategory(existingCategory, updateCategoryDto, userId);

            return true;
        }

        public async Task<IEnumerable<CategoryListDto>> GetCategoriesAsAList()
        {
            try
            {
                var existingCategories = await _dapperRepository.GetCategoriesAsList();
                if (existingCategories is null)
                    throw new NotFoundException(string.Format("Categories not found"));

                return existingCategories;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetCategoriesAsAList), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        
        }


        public async Task<PagedListResponse<IEnumerable<CategoryDto>>> GetAllRecords(RequestTableBodyDto filter)
        {
            try
            {
                var categoryWithMetaData = await _dapperRepository.SearchCategories(filter);
                var categoryDtoList = _mapper.Map<IEnumerable<CategoryDto>>(categoryWithMetaData);
                var columns = GetDataTableColumns();

                PagedListResponse<IEnumerable<CategoryDto>> response = new PagedListResponse<IEnumerable<CategoryDto>>
                {
                    TotalCount = categoryWithMetaData.MetaData.TotalCount,
                    CurrentPage = categoryWithMetaData.MetaData.CurrentPage,
                    PageSize = categoryWithMetaData.MetaData.PageSize,
                    Columns = columns,
                    Rows = categoryDtoList,
                    Key = categoryWithMetaData.MetaData.Key,
                    hasNext = categoryWithMetaData.MetaData.hasNext,
                    hasPrevious = categoryWithMetaData.MetaData.hasPrevious,
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetAllRecords), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var existingCategory = await _repositoryManager.Category.GetCategory(id);
            return existingCategory;
        }


        public async Task UpdateCategory(Category existingCategory, CategoryForCreationAndUpdateDto updateCategoryDto, int userId)
        {
            try
            {
                _mapper.Map(updateCategoryDto, existingCategory);

                existingCategory.DateModified = DateTime.UtcNow;
                existingCategory.ModifiedBy = userId;
                _repositoryManager.Category.UpdateCategory(existingCategory);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(updateCategoryDto), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        private List<DataTableColumn> GetDataTableColumns()
        {
            var columns = GenerateDataTableColumn<CategoryColumn>.GetDataTableColumns();
            return columns;
        }

    }
}
