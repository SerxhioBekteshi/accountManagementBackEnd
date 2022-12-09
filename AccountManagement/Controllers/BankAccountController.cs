using AccountManagement.Utilities;
using AccountManagement.Validation;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public BankAccountController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _service = service; 
        }

        [HttpPost("get-all"),Authorize]
        public async Task<IActionResult> GetAllBankAccounts([FromBody] RequestTableBodyDto request)
        {
            //var bankAccountsFromDb = await _repository.BankAccount.GetAllBankAccountsAsync(ClaimsUtility.ReadCurrentuserId(User.Claims));

            //if (bankAccountsFromDb == null)
            //{
            //    _logger.LogInfo("No bank accounts available");
            //    var response = new BaseResponse<String, object>
            //    {
            //        data = null,
            //        successMessage = "",
            //        errorMessage = "No bank accounts available. Create one!",
            //        result = false,
            //        StatusCode = 400,
            //    };

            //    return Ok(response);

            //}

            //var bankAccountDto = _mapper.Map<IEnumerable<BankAccountDto>>(bankAccountsFromDb);
            //var response2 = new BaseResponse<IEnumerable<BankAccountDto>, object>
            //{
            //    data = (IEnumerable<BankAccountDto>)bankAccountDto,
            //    result = true,
            //    errorMessage = "",
            //    successMessage = "",
            //    StatusCode = 200
            //};

            //return Ok(response2);

            var bankAccoutsFromDb = await _service.BankAccountService.GetAllRecords( Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)), request);

            var baseResponse = new BaseResponse<IEnumerable<BankAccountDto>, object>
            {
                result = true,
                data = bankAccoutsFromDb,
                successMessage = "",
                errorMessage = "",
                StatusCode = 200,
                Errors = "",

            };

            return Ok(baseResponse);
        }

        [HttpGet("{id}"), Authorize]

        public async Task<IActionResult> GetBankAccount(int id)
        {
            //var bankAccountFromDb = await _repository.BankAccount.GetBankAccountAsync(id, ClaimsUtility.ReadCurrentuserId(User.Claims));
            //if (bankAccountFromDb == null)
            //{
            //    _logger.LogInfo($"The bank account with {id} does not exist in the database");
            //    return NotFound();
            //}
            //var bankAccountDto = _mapper.Map<BankAccountDto>(bankAccountFromDb);
            //var response = new BaseResponse<BankAccountDto, object>
            //{
            //    data = bankAccountDto,
            //    result = true,
            //    errorMessage = "",
            //    successMessage = "",
            //    StatusCode = 200
            //};
            //return Ok(response);

            var result = await _service.BankAccountService.GetRecordById(id);
            var baseResponse = new BaseResponse<object, object>
            {
                result = true,
                data = result,
                errorMessage = "",
                successMessage = "",
                StatusCode = 200,
                Errors = ""
            };

            return Ok(baseResponse);

        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateBankAccount([FromBody] BankAccountForCreationAndUpdateDto bankAccountForCreateDto)
        {

            //if (bankAccountForCreateDto == null)
            //{
            //    _logger.LogError("The bank account for creation Dto should not be null");
            //    return BadRequest("The bank account for creation Dto should not be null");
            //}

            //var bankAccountEntity = _mapper.Map<BankAccount>(bankAccountForCreateDto);
            //bankAccountEntity.IsActive = true;
            //bankAccountEntity.DateCreated = DateTime.Now;
            //bankAccountEntity.ClientId = int.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims));
            //_repository.BankAccount.CreateBankAccount(bankAccountEntity);
            //await _repository.SaveAsync();

            //var response = new BaseResponse<String, object>
            //{
            //    data = "",
            //    result = true,
            //    errorMessage = "",
            //    successMessage = "Bank Account Created successfully",
            //    StatusCode = 200
            //};
            //return Ok(response);

            BankAccountValidation validator = new BankAccountValidation();
            var validatorResult = validator.Validate(bankAccountForCreateDto);
            if (!validatorResult.IsValid)
            {
                var baseResponse = new BaseResponse<object, List<ValidationFailure>>
                {
                    result = false,
                    data = "",
                    Errors = validatorResult.Errors,
                    StatusCode = 400,
                    successMessage = "",
                    errorMessage = ""
                };
                return Ok(baseResponse);
            }
            else
            {

                var result = await _service.BankAccountService.CreateRecord(bankAccountForCreateDto, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

                var baseResponse = new BaseResponse<object, object>
                {
                    result = true,
                    data = result,
                    errorMessage = "",
                    successMessage = "Bank Account Added successfully",
                    StatusCode = 200,
                    Errors = ""
                };
                return Ok(baseResponse);

            }
        }

        [HttpPut("{id}"),Authorize]

        public async Task<IActionResult> UpdateBankAccount(int id, [FromBody] BankAccountForCreationAndUpdateDto bankAccountForUpdateDto)
        {

            //if (bankAccountForCreateDto == null)
            //{
            //    _logger.LogError("The bank account for update Dto should not be null");
            //    return BadRequest("The bank account for update Dto should not be null");
            //}
            //var bankAccountFromDb = await _repository.BankAccount.GetBankAccountAsync(id, ClaimsUtility.ReadCurrentuserId(User.Claims));

            //if (bankAccountFromDb == null)
            //{
            //    _logger.LogInfo($"The bank account with {id} does not exist in the database");
            //    return NotFound();
            //}
            //bankAccountFromDb.IsActive = true;
            //bankAccountFromDb.DateModified = DateTime.Now;
            //_mapper.Map(bankAccountForCreateDto, bankAccountFromDb);
            //await _repository.SaveAsync();

            //var response = new BaseResponse<String, object>
            //{
            //    data = "",
            //    result = true,
            //    errorMessage = "",
            //    successMessage = "Bank Account Updated successfully",
            //    StatusCode = 200
            //};
            //return Ok(response);

            BankAccountValidation validator = new BankAccountValidation();
            var validatorResult = validator.Validate(bankAccountForUpdateDto);
            if (!validatorResult.IsValid)
            {
                var baseResponse = new BaseResponse<object, List<ValidationFailure>>
                {
                    result = false,
                    data = "",
                    Errors = validatorResult.Errors,
                    StatusCode = 400,
                    successMessage = "",
                    errorMessage = ""
                };
                return Ok(baseResponse);
            }
            else
            {
                var result = await _service.BankAccountService.UpdateRecord(id, bankAccountForUpdateDto, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

                var baseResponse = new BaseResponse<object, object>
                {
                    result = result,
                    data = "",
                    errorMessage = "",
                    successMessage = "Updated Successfully",
                    StatusCode = 200,
                    Errors = "",
                };
                return Ok(baseResponse);
            }

        }

        [HttpDelete("{id}"), Authorize]

        public async Task<IActionResult> DeleteProduct(int id)
        {

            var result = await _service.BankAccountService.DeleteRecord(id);
            var baseResponse = new BaseResponse<object, object>
            {
                result = result,
                data = "",
                errorMessage = "",
                successMessage = "Deleted Successfully",
                StatusCode = 200,
                Errors = "",
            };
            return Ok(baseResponse);

        }
    }
}