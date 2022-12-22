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
    public class BankAccountService : IBankAccountService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapper;

        public BankAccountService(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager, IDapperRepository dapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _dapper = dapper;
        }

        public async Task<bool> CreateRecord(BankAccountCreateUpdateDto createBankAccountDto, int userId)
        {
            try
            {
                var bankAccount = _mapper.Map<BankAccount>(createBankAccountDto);
                bankAccount.DateCreated = DateTime.UtcNow;
                bankAccount.CreatedBy = userId;

                _repositoryManager.BankAccount.CreateBankAccount(bankAccount);
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
                var existingBankAccount = await GetBankAccountAndCheckIfExistsAsync(id);

                _repositoryManager.BankAccount.DeleteBankAccount(existingBankAccount);
                await _repositoryManager.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRecord), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool> UpdateRecord(BankAccountCreateUpdateDto updateBankAccountDto, int id, int userId)
        {
            try
            {
                var existingBankAccount = await GetBankAccountAndCheckIfExistsAsync(id);

                _mapper.Map(updateBankAccountDto, existingBankAccount);

                existingBankAccount.DateModified = DateTime.UtcNow;
                existingBankAccount.ModifiedBy = userId;

                _repositoryManager.BankAccount.UpdateBankAccount(existingBankAccount);
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
        private async Task<BankAccount> GetBankAccountAndCheckIfExistsAsync(int id)
        {
            var existingBankAccount = await _repositoryManager.BankAccount.GetBankAccountAsync(id);
            if (existingBankAccount is null)
                throw new NotFoundException(string.Format("Connection with Id: {0} between bank and user was not found!", id));

            return existingBankAccount;
        }

        #endregion
 
    }
}
