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
    public class CurrencyBankService : ICurrencyBankService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapper;

        public CurrencyBankService(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager, IDapperRepository dapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _dapper = dapper;
        }

        public async Task<bool> CreateRecord(CurrencyBankCreateUpdateDto createCurrencyBankDto, int userId)
        {
            try
            {
                var currencyBank = _mapper.Map<CurrencyBank>(createCurrencyBankDto);
                currencyBank.DateCreated = DateTime.UtcNow;
                currencyBank.CreatedBy = userId;

                _repositoryManager.CurrencyBank.CreateRecord(currencyBank);
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
                var existingCurrencyBank = await GetCurrencyBankAndCheckIfExistsAsync(id);

                _repositoryManager.CurrencyBank.DeleteRecord(existingCurrencyBank);
                await _repositoryManager.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRecord), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(CurrencyBankCreateUpdateDto updateCurrencyBankDto, int id, int userId)
        {
            try
            {
                var existingCurrencyBank = await GetCurrencyBankAndCheckIfExistsAsync(id);

                _mapper.Map(updateCurrencyBankDto, existingCurrencyBank);

                existingCurrencyBank.DateModified = DateTime.UtcNow;
                existingCurrencyBank.ModifiedBy = userId;

                _repositoryManager.CurrencyBank.UpdateRecord(existingCurrencyBank);
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
        private async Task<CurrencyBank> GetCurrencyBankAndCheckIfExistsAsync(int id)
        {
            var existingCurrencyAndBank = await _repositoryManager.CurrencyBank.GetRecordByIdAsync(id);
            if (existingCurrencyAndBank is null)
                throw new NotFoundException(string.Format("Connection with Id: {0} between currency and bank was not found!", id));

            return existingCurrencyAndBank;
        }

        #endregion
    }
}
