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
    public class CurrencyService : ICurrencyService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapperRepository;

        public CurrencyService(ILoggerManager logger,
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

        public async Task<PagedListResponse<IEnumerable<CurrencyDto>>> GetAllRecords(RequestTableBodyDto request)
        {
            try
            {
                var currencyWithMetaData = await _dapperRepository.SearchCompanies(request);
                var currencyDtoList = _mapper.Map<IEnumerable<CurrencyDto>>(currencyWithMetaData);
                var columns = GetDataTableColumns();

                PagedListResponse<IEnumerable<CurrencyDto>> response = new PagedListResponse<IEnumerable<CurrencyDto>>
                {
                    TotalCount = currencyWithMetaData.MetaData.TotalCount,
                    CurrentPage = currencyWithMetaData.MetaData.CurrentPage,
                    PageSize = currencyWithMetaData.MetaData.PageSize,
                    Columns = columns,
                    Rows = currencyDtoList,
                    Key = currencyWithMetaData.MetaData.Key,
                    hasNext = currencyWithMetaData.MetaData.hasNext,
                    hasPrevious = currencyWithMetaData.MetaData.hasPrevious,

                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetAllRecords), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }


        public async Task<int> CreateRecord(CurrencyForCreationAndUpdateDto currencyCreateDto, int userId)
        {
            try
            {
                var currency = _mapper.Map<Currency>(currencyCreateDto);

                currency.DateCreated = DateTime.UtcNow;
                currency.CreatedBy = userId;

                _repositoryManager.Currency.CreateCurrency(currency);
                await _repositoryManager.SaveAsync();


                CreateCurrencyBankRelation(currencyCreateDto.BankIds, currency.Id, userId);
                await _repositoryManager.SaveAsync();


                return currency.Id;
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

                var existingCurrency = await GetCurrencyIdAsync(id);

                if (existingCurrency != null)
                {
                    await DeleteCurrencyBankRelationForCurrencyId(id);
                    _repositoryManager.Currency.DeleteCurrency(existingCurrency);
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

        public async Task<bool> DeleteRelationCurrency(int bankId, int currencyId)
        {
            try
            {
                var existingRelation = await _repositoryManager.CurrencyBank.GetRecordByCurrencyIdBankIdAsync(currencyId, bankId);
                if (existingRelation is null)
                    throw new NotFoundException(string.Format("There is no relation between bank with Id: {0} and currency with Id: {1}!", bankId, currencyId));

                _repositoryManager.CurrencyBank.DeleteRecord(existingRelation);


                await _repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRelationCurrency), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        private List<DataTableColumn> GetDataTableColumns()
        {
            var columns = GenerateDataTableColumn<CurrencyBank>.GetDataTableColumns();
            return columns;
        }
        public async Task<CurrencyDto> GetRecordById(int id)
        {

            try
            {
                var existingCurrency = await _dapperRepository.GetCurrencyById(id);
                if (existingCurrency is null)
                    throw new NotFoundException(string.Format("Currency with Id: {0} was not found!", id));

                var banksIdForCurrency = await _repositoryManager.CurrencyBank.GetBankIdsForCurrencyId(id);
                if (banksIdForCurrency.Any())
                    existingCurrency.BankIds = banksIdForCurrency.ToList();

                return existingCurrency;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetRecordById), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> PostBanksForCurrency(int currencyId, PostCurrenciesToBankDto postBanksToCurrencyId, int managerId)
        {
            try
            {
                var existingCurrency = await GetCurrencyIdAsync(currencyId);

                if (existingCurrency is null)
                    throw new NotFoundException(string.Format("Currency with id: {0} not found!", currencyId));

                foreach (var bankId in postBanksToCurrencyId.BankIds)
                {
                    var existingCategory = await _repositoryManager.Category.GetCategory((int)currencyId);
                    if (existingCategory is null)
                    {
                        throw new NotFoundException(string.Format("Category with id: {0} not found! Please contact administrator!", bankId));
                    }
                }

                _mapper.Map(postBanksToCurrencyId, existingCurrency);

                var currencyBankIds = await _repositoryManager.CurrencyBank.GetBankIdsForCurrencyId(currencyId);
                var banksToAdd = postBanksToCurrencyId?.BankIds?.Where(x => !currencyBankIds.Contains(x)).ToList();

                if (banksToAdd.Count == 0)
                {
                    await DeleteCurrencyBankRelationForCurrencyId(currencyId);
                    var categoriesToAdd2 = postBanksToCurrencyId?.BankIds.ToList();
                    CreateCurrencyBankRelation(categoriesToAdd2, currencyId, managerId);
                }
                else
                    CreateCurrencyBankRelation(banksToAdd, currencyId, managerId);


                await _repositoryManager.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(PostBanksForCurrency), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<IEnumerable<BankAccountDto>> GetBanksForCurrency(int currencyId)
        {
            try
            {
                var existingCurrency = await GetCurrencyIdAsync(currencyId);

                if (existingCurrency is null)
                    throw new NotFoundException(string.Format("Currency with id: {0} not found!", currencyId));

                var currencyBanks = await _dapperRepository.GetBanksForCurrencyId(currencyId);

                return currencyBanks;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetBanksForCurrency), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(int id, CurrencyForCreationAndUpdateDto updateCurrencyDto, int userId)
        {
            var existingCurrency = await GetCurrencyIdAsync(id);

            var existingCurrencyCategory = existingCurrency.CurrencyBankAccount;

            await UpdateCurrency(existingCurrency, updateCurrencyDto, userId);

            await UpdateCurrencyBanks (existingCurrencyCategory, updateCurrencyDto, id, userId);


            return true;
        }

        #region Private Methods
        private async Task<Currency> GetCurrencyIdAsync(int id)
        {
            var existingCurrency = await _repositoryManager.Currency.GetCurrencyAsync(id, trackChanges: false);
            return existingCurrency;
        }

        private async Task UpdateCurrency(Currency existingCurrency, CurrencyForCreationAndUpdateDto updateCurrencyDto, int userId)
        {
            try
            {
                _mapper.Map(updateCurrencyDto, existingCurrency);

                existingCurrency.DateModified = DateTime.UtcNow;
                existingCurrency.ModifiedBy = userId;
                _repositoryManager.Currency.UpdateCurrency(existingCurrency);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateCurrency), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }


        private void CreateCurrencyBankRelation(List<int?>? bankIds, int currencyId, int userId)
        {
            if (bankIds is not null)
            {
                foreach (var bank in bankIds)
                {
                    var newCurrencyBank = new CurrencyBank
                    {
                        BankId = (int)bank,
                        CurrencyId = currencyId,
                        DateCreated = DateTime.UtcNow,
                        CreatedBy = userId
                    };
                    _repositoryManager.CurrencyBank.CreateRecord(newCurrencyBank);
                }
            }
        }

        private async Task DeleteCurrencyBankRelationForCurrencyId(int currencyId)
        {
            var existingCurrencyBank = await _repositoryManager.CurrencyBank.GetCurrencyBankAccountForCurrencyIdAsync(currencyId);
            foreach (var existingBank in existingCurrencyBank)
            {
                _repositoryManager.CurrencyBank.DeleteRecord(existingBank);
            }
        }

        private async Task UpdateCurrencyBanks(List<CurrencyBank> existingCurrencyBank, CurrencyForCreationAndUpdateDto updateCurrencyDto, int currencyId, int userId)
        {
            try
            {

                if (existingCurrencyBank != null && existingCurrencyBank.Count() > 0 && updateCurrencyDto.BankIds is not null)
                {
                    var ids = existingCurrencyBank.Select(x => x.BankId).ToList();
                    var check = ids.Except(updateCurrencyDto.BankIds);

                    if (check.Count() != 0)
                    {
                        await DeleteCurrencyBankRelationForCurrencyId(currencyId);
                        CreateCurrencyBankRelation(updateCurrencyDto.BankIds, currencyId, userId);
                    }
                }
                else
                {
                    if (updateCurrencyDto.BankIds is not null)
                        CreateCurrencyBankRelation(updateCurrencyDto.BankIds, currencyId, userId);
                }

                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(UpdateCurrencyBanks), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        Task<bool> ICurrencyService.PostCurrenciesForBank(int bankId, PostCurrenciesToBankDto postCurrenciesToBankId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

