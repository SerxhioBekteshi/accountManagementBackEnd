using AutoMapper;
using Contracts;
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
    public class BankAccountService : IBankAccountService
    {

        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapperRepository;
        public BankAccountService(ILoggerManager logger,
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
        public async Task<int> CreateRecord(BankAccountForCreationAndUpdateDto createBankAccountDTO, int userId)
        {
            try
            {
                var bankAccount = _mapper.Map<BankAccount>(createBankAccountDTO);

                bankAccount.DateCreated = DateTime.UtcNow;
                bankAccount.CreatedBy = userId;
                bankAccount.ClientId = userId;

                _repositoryManager.BankAccount.CreateBankAccount(bankAccount);
                await _repositoryManager.SaveAsync();

                return bankAccount.Id;

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
                var existingBankAccount = await GetBankAccountByIdAsync(id);
                if (existingBankAccount != null)
                {
                    _repositoryManager.BankAccount.DeleteBankAccount(existingBankAccount);
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

        public async Task<IEnumerable<BankAccountDto>> GetAllRecords(int userId, RequestTableBodyDto request)
        {
           try
            {
                var existingBankAccounts = await _dapperRepository.GetBankAccountsForLoggedUser(userId, request);
                if (existingBankAccounts is null)
                    throw new NotFoundException(string.Format("No Bank Accounts"));

                return existingBankAccounts;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetRecordById), ex.Message));
                throw new BadRequestException(ex.Message);
            }

        }

        public async Task<BankAccountDto> GetRecordById(int id)
        {
            try
            {
                var existingBankAccount = await _dapperRepository.GetBankAccountById(id);
                if (existingBankAccount is null)
                    throw new NotFoundException(string.Format("Bank Account with Id: {0} was not found!", id));

                return existingBankAccount;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetRecordById), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(int id, BankAccountForCreationAndUpdateDto updateBankAccountDto, int userId)
        {
            var existingBankAccount = await GetBankAccountByIdAsync(id);

            await UpdateBankAccount(existingBankAccount, updateBankAccountDto, userId);

            return true;
        }

        private async Task<BankAccount> GetBankAccountByIdAsync(int id)
        {
            var existingBankAccount = await _repositoryManager.BankAccount.GetBankAccountAsync(id);
            return existingBankAccount;
        }


        private async Task UpdateBankAccount(BankAccount existingBankAccount, BankAccountForCreationAndUpdateDto updateBankAccountDto, int userId)
        {
            try
            {
                _mapper.Map(updateBankAccountDto, existingBankAccount);

                existingBankAccount.DateModified = DateTime.UtcNow;
                existingBankAccount.ModifiedBy = userId;
                _repositoryManager.BankAccount.UpdateBankAccount(existingBankAccount);
                await _repositoryManager.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(updateBankAccountDto), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

    }
}
