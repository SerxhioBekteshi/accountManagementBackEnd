using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using SendGrid.Helpers.Errors.Model;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CompanyCategoryService : ICompanyCategoryService
    {

        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapper;

        public CompanyCategoryService(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager, IDapperRepository dapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _dapper = dapper;
        }

        public async Task<bool> CreateRecord(CreateCompanyCategoryDto createCompanyCategoryDto, int userId)
        {
            try
            {
                var serviceLocation = _mapper.Map<CompanyCategory>(createCompanyCategoryDto);
                serviceLocation.DateCreated = DateTime.UtcNow;
                serviceLocation.CreatedBy = userId;

                _repositoryManager.CompanyCategory.CreateRecord(serviceLocation);
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
                var existingServiceLocation = await GetCompanyCategoryAndCheckIfExistsAsync(id);

                _repositoryManager.CompanyCategory.DeleteRecord(existingServiceLocation);
                await _repositoryManager.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRecord), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(UpdateCompanyCategoryDto updateServiceLocationDto, int id, int userId)
        {
            try
            {
                var existingServiceLocaton = await GetCompanyCategoryAndCheckIfExistsAsync(id);

                _mapper.Map(updateServiceLocationDto, existingServiceLocaton);

                existingServiceLocaton.DateModified = DateTime.UtcNow;
                existingServiceLocaton.ModifiedBy = userId;

                _repositoryManager.CompanyCategory.UpdateRecord(existingServiceLocaton);
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
        private async Task<CompanyCategory> GetCompanyCategoryAndCheckIfExistsAsync(int id)
        {
            var existingServiceLocation = await _repositoryManager.CompanyCategory.GetRecordByIdAsync(id);
            if (existingServiceLocation is null)
                throw new NotFoundException(string.Format("Connection with Id: {0} between category and company was not found!", id));

            return existingServiceLocation;
        }

        #endregion
    }
}
