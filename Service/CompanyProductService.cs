using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using SendGrid.Helpers.Errors.Model;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CompanyProductService : ICompanyProductService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapper;

        public CompanyProductService(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager, IDapperRepository dapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _dapper = dapper;
        }

        public async Task<bool> CreateRecord(CompanyProductCreateUpdateDto createCompanyProductDto, int userId)
        {
            try
            {
                var companyProduct = _mapper.Map<CompanyProduct>(createCompanyProductDto);
                companyProduct.DateCreated = DateTime.UtcNow;
                companyProduct.CreatedBy = userId;

                _repositoryManager.CompanyProduct.CreateRecord(companyProduct);
                await _repositoryManager.SaveAsync();

                return true;
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
                var existingCompanyProduct = await GetCompanyProductAndCheckIfExistsAsync(id);

                _repositoryManager.CompanyProduct.DeleteRecord(existingCompanyProduct);
                await _repositoryManager.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRecord), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(CompanyProductCreateUpdateDto updateCompanyProductDto, int id, int userId)
        {
            try
            {
                var existingCompanyProduct = await GetCompanyProductAndCheckIfExistsAsync(id);

                _mapper.Map(updateCompanyProductDto, existingCompanyProduct);

                existingCompanyProduct.DateModified = DateTime.UtcNow;
                existingCompanyProduct.ModifiedBy = userId;

                _repositoryManager.CompanyProduct.UpdateRecord(existingCompanyProduct);
                await _repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateRecord), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        #region Private Methods
        private async Task<CompanyProduct> GetCompanyProductAndCheckIfExistsAsync(int id)
        {
            var existingCompanyProduct = await _repositoryManager.CompanyProduct.GetRecordByIdAsync(id);
            if (existingCompanyProduct is null)
                throw new NotFoundException(string.Format("Connection with Id: {0} between product and company was not found!", id));

            return existingCompanyProduct;
        }

        #endregion
    }
}
