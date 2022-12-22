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
    public class BankController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public BankController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _service = service; 
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllBanks([FromBody] RequestTableBodyDto request)
        {
            var bankAccoutsFromDb = await _service.BankService.GetAllRecords( request);

            var baseResponse = new BaseResponse<PagedListResponse<IEnumerable<BankDto>>, object>
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

            var result = await _service.BankService.GetRecordById(id);
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
        public async Task<IActionResult> CreateBankAccount([FromBody] BankForCreationAndUpdateDto bankAccountForCreateDto)
        {

          
            BankValidation validator = new BankValidation();
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

                var result = await _service.BankService.CreateRecord(bankAccountForCreateDto, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

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

        public async Task<IActionResult> UpdateBankAccount(int id, [FromBody] BankForCreationAndUpdateDto bankAccountForUpdateDto)
        {

            BankValidation validator = new BankValidation();
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
                var result = await _service.BankService.UpdateRecord(id, bankAccountForUpdateDto, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

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

            var result = await _service.BankService.DeleteRecord(id);
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


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBankAccounts()
        {
            var bankAccoutsFromDb = await _service.BankService.GetAllBanksPerLoggedUser( Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));
            var baseResponse = new BaseResponse<IEnumerable<BankDto>, object>
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

    }
}