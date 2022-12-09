using AutoMapper;
using Contracts;
using Entities.DTC;
using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using SendGrid.Helpers.Errors.Model;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapperRepository;
        public ProductService(ILoggerManager logger,
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

        public async Task<int> CreateRecord(ProductForCreationAndUpdateDto createProductDTO, int userId)
        {
            try
            {
                var product = _mapper.Map<Product>(createProductDTO);

                product.DateCreated = DateTime.UtcNow;
                product.CreatedBy = userId;

                _repositoryManager.Product.CreateProduct(product);
                await _repositoryManager.SaveAsync();

                return product.Id;

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
                var existingProduct = await GetProductByIdAsync(id);
                if (existingProduct != null)
                {
                    _repositoryManager.Product.DeleteProduct(existingProduct);
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

        public async Task<PagedListResponse<IEnumerable<ProductDto>>> GetAllRecords(RequestTableBodyDto request)
        {

            try
            {
                var productWithMetaData = await _dapperRepository.SearchProducts(request);
                var productDtoList = _mapper.Map<IEnumerable<ProductDto>>(productWithMetaData);
                var columns = GetDataTableColumns();

                PagedListResponse<IEnumerable<ProductDto>> response = new PagedListResponse<IEnumerable<ProductDto>>
                {
                    TotalCount = productWithMetaData.MetaData.TotalCount,
                    CurrentPage = productWithMetaData.MetaData.CurrentPage,
                    PageSize = productWithMetaData.MetaData.PageSize,
                    Columns = columns,
                    Rows = productDtoList,
                    Key = productWithMetaData.MetaData.Key,
                    hasNext = productWithMetaData.MetaData.hasNext,
                    hasPrevious = productWithMetaData.MetaData.hasPrevious,

                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetAllRecords), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<ProductDto> GetRecordById(int id)
        {
            try
            {
                var existingProduct = await _dapperRepository.GetProductById(id);
                if (existingProduct is null)
                    throw new NotFoundException(string.Format("Product with Id: {0} was not found!", id));

                return existingProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetRecordById), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(int id, ProductForCreationAndUpdateDto updateProductDto, int userId)
        {
            var existingProduct = await GetProductByIdAsync(id);

            await UpdateProduct(existingProduct, updateProductDto, userId);

            return true;
        }


        private async Task<Product> GetProductByIdAsync(int id)
        {
            var existingProduct = await _repositoryManager.Product.GetProductAsync(id);
            return existingProduct;
        }


        private async Task UpdateProduct(Product existingProduct, ProductForCreationAndUpdateDto updateProductDto, int userId)
        {
            try
            {
                _mapper.Map(updateProductDto, existingProduct);

                existingProduct.DateModified = DateTime.UtcNow;
                existingProduct.ModifiedBy = userId;
                _repositoryManager.Product.UpdateProduct(existingProduct);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(updateProductDto), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        private List<DataTableColumn> GetDataTableColumns()
        {
            var columns = GenerateDataTableColumn<ProductColumn>.GetDataTableColumns();
            return columns;
        }
    }
}
