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
    public class BankService : IBankService
    {

        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapperRepository;
        public BankService(ILoggerManager logger,
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
        public async Task<int> CreateRecord(BankForCreationAndUpdateDto createBankDto, int userId)
        {
            try
            {
                var bank = _mapper.Map<Bank>(createBankDto);

                bank.DateCreated = DateTime.UtcNow;
                bank.CreatedBy = userId;

                _repositoryManager.Bank.CreateBank(bank);
                await _repositoryManager.SaveAsync();

                return bank.Id;

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
                var existingBank = await GetBankAccountByIdAsync(id);
                if (existingBank != null)
                {
                    _repositoryManager.Bank.DeleteBank(existingBank);
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

        public async Task<PagedListResponse<IEnumerable<BankDto>>> GetAllRecords(RequestTableBodyDto request)
        {
            try
            {
                var banksWithMetaData = await _dapperRepository.SearchBanks(request);
                var banksDtoList = _mapper.Map<IEnumerable<BankDto>>(banksWithMetaData);
                var columns = GetDataTableColumns();

                PagedListResponse<IEnumerable<BankDto>> response = new PagedListResponse<IEnumerable<BankDto>>
                {
                    TotalCount = banksWithMetaData.MetaData.TotalCount,
                    CurrentPage = banksWithMetaData.MetaData.CurrentPage,
                    PageSize = banksWithMetaData.MetaData.PageSize,
                    Columns = columns,
                    Rows = banksDtoList,
                    Key = banksWithMetaData.MetaData.Key,
                    hasNext = banksWithMetaData.MetaData.hasNext,
                    hasPrevious = banksWithMetaData.MetaData.hasPrevious,
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetAllRecords), ex.Message));
                throw new BadRequestException(ex.Message);
            }

        }

        public async Task<BankDto> GetRecordById(int id)
        {
            try
            {
                var existingBank = await _dapperRepository.GetBankById(id);
                if (existingBank is null)
                    throw new NotFoundException(string.Format("Bank with Id: {0} was not found!", id));

                return existingBank;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetRecordById), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(int id, BankForCreationAndUpdateDto updateBankDto, int userId)
        {
            var existingBank = await GetBankAccountByIdAsync(id);

            await UpdateBank(existingBank, updateBankDto, userId);

            return true;
        }

        public async Task<IEnumerable<BankListDto>> GetBanksAsAList()
        {
            try
            {
                var existingBanks = await _dapperRepository.GetBanksAsList();
                if (existingBanks is null)
                    throw new NotFoundException(string.Format("Categories not found"));

                return existingBanks;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetBanksAsAList), ex.Message));
                throw new BadRequestException(ex.Message);
            }

        }

        public async Task<IEnumerable<BankDto>> GetAllBanksPerLoggedUser(int userId)
        {
            try
            {
                var existingBanks = await _dapperRepository.GetBanksPerUser(userId);
                if (existingBanks is null)
                    throw new NotFoundException(string.Format("Banks not found"));

                return existingBanks;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetAllBanksPerLoggedUser), ex.Message));
                throw new BadRequestException(ex.Message);
            }


        }


        private async Task<Bank> GetBankAccountByIdAsync(int id)
        {
            var existingBankAccount = await _repositoryManager.Bank.GetBankAsync(id);
            return existingBankAccount;
        }


        private async Task UpdateBank(Bank existingBank, BankForCreationAndUpdateDto updateBankAccountDto, int userId)
        {
            try
            {
                _mapper.Map(updateBankAccountDto, existingBank);

                existingBank.DateModified = DateTime.UtcNow;
                existingBank.ModifiedBy = userId;
                _repositoryManager.Bank.UpdateBank(existingBank);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(updateBankAccountDto), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }





        private List<DataTableColumn> GetDataTableColumns()
        {
            var columns = GenerateDataTableColumn<BankColumn>.GetDataTableColumns();
            return columns;
        }
    }
}
