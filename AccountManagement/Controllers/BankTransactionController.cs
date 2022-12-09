using AccountManagement.Utilities;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.ResponseFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public BankTransactionController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpPost,Authorize]
        public async Task<IActionResult> CreateTransaction([FromBody] BankTransactionForCreateDto bankTransactionForCreateDto)
        {
            //nqs objekti null
            if(bankTransactionForCreateDto == null)
            {
                _logger.LogError("Object can not be null");
                return BadRequest("Object can not be null");
            }

            if(bankTransactionForCreateDto.Action < 0 || bankTransactionForCreateDto.Action > 1)
            {
                _logger.LogError("Action can not be other than Deposit or Withdraw");
                return BadRequest("Action can not be other than Deposit or Withdraw");
            }
            if (await _repository.BankAccount.GetBankAccountAsync(bankTransactionForCreateDto.BankAccountId) == null)
            {
                _logger.LogInfo($"The bank account with {bankTransactionForCreateDto.BankAccountId} does not exist in the database");
                return NotFound();
            }

            //marr llogarine bankare nga e cila dua te bej transaksion
            var BankAccountToDoTransactionFromDb = await _repository.BankAccount.
                GetBankAccountAsync(bankTransactionForCreateDto.BankAccountId);

            //GetBankAccountAsync(bankTransactionForCreateDto.BankAccountId, ClaimsUtility.ReadCurrentuserId(User.Claims)); DUHET DHE ME SIPER TE KUSHTI ME NULl


            //kontrolloj nqs shuma qe dua te bej transaksion eshte me e madhe se balance ne llogari, nqs po nxjerr error 
            if (bankTransactionForCreateDto.Amount > BankAccountToDoTransactionFromDb.Balance)
            {
                var response2 = new BaseResponse<String, object>
                {
                    data = "",
                    result = true,
                    errorMessage = "",
                    successMessage = "You have no money in your bank account to do this transaction",
                    StatusCode = 200
                };
                return Ok(response2);
            }
            //ne te kundertet mapoj dto ndaj entitetit, i jap daten aktuale kur u be dhe e bej aktive, ruaj ndryshimet ne DB
            var bankTransactionEntity = _mapper.Map<BankTransaction>(bankTransactionForCreateDto);
            bankTransactionEntity.DateCreated = DateTime.Now;
            bankTransactionEntity.IsActive = true;
            _repository.BankTransaction.CreateBankTransaction(bankTransactionEntity);
            await _repository.SaveAsync();


            //pasi ruaj ndryshimet ne Db bej zbrjtjen ose shumen ne varesi te actionit per balanca
            if(bankTransactionForCreateDto.Action  == 0)
            {
                BankAccountToDoTransactionFromDb.Balance -= bankTransactionForCreateDto.Amount;
            }
            else if( bankTransactionForCreateDto.Action == 1)
            {
                BankAccountToDoTransactionFromDb.Balance -= bankTransactionForCreateDto.Amount;
            }

            _repository.BankAccount.UpdateBankAccount(BankAccountToDoTransactionFromDb);
            await _repository.SaveAsync();

            var response = new BaseResponse<String, object>
            {
                data = "",
                result = true,
                errorMessage = "",
                successMessage = "Transaction was made successfully",
                StatusCode = 200
            };
            return Ok(response);

        }

        [HttpGet, Authorize]

        public async Task<IActionResult> GetBankTransactions ()
        {
            var bankTransactions = await _repository.BankTransaction.GetAllBankAccountTransactionsAsync(trackChanges: false);
            if(bankTransactions == null)
            {
                _logger.LogInfo("There are not bank transactions available");
                return NotFound();
            }

            var bankTransactionsDto = _mapper.Map<IEnumerable<BankTransactionDto>>(bankTransactions);
            var response = new BaseResponse<IEnumerable<BankTransactionDto>, object>
            {
                data = bankTransactionsDto,
                result = true,
                errorMessage = "",
                successMessage = "",
                StatusCode = 200
            };
            return Ok(response);
        }
    }
}
