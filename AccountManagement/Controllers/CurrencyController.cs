using AccountManagement.Utilities;
using AccountManagement.Validation;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;
        public CurrencyController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllCurrencies ([FromBody] RequestTableBodyDto request)
        {
            var currenciesFromDb = await _service.CurrencyService.GetAllRecords(request);
            var response = new BaseResponse<object, object>
            { 
                data = currenciesFromDb, 
                errorMessage = "", 
                successMessage = "", 
                StatusCode = 200, 
                result = true
            };

            return Ok(response);

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCurrency (int id)
        {
            var currency = await _service.CurrencyService.GetRecordById(id);
            var response = new BaseResponse<CurrencyDto, object>
            {
                data = currency,
                errorMessage = "",
                successMessage = "",
                StatusCode = 200,
                result = true,
                Errors = ""
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCurrency (int id)
        {
            var result = await _service.CurrencyService.DeleteRecord(id);

            var response = new BaseResponse<object, object>
            {
                data = result,
                errorMessage = "",
                successMessage = "Currency deleted successfully",
                StatusCode = 200,
                result = true,
            };

            return Ok(response);

        }

        [HttpGet("{id}/banks")]

        public async Task<IActionResult> GetBanksForCurrency(int id)
        {
            var result = await _service.CurrencyService.GetBanksForCurrency(id);    

            var response = new BaseResponse<object, object>
            {
                data = result,
                errorMessage = "",
                successMessage = "",
                StatusCode = 200,
                result = true,
            };

            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyForCreationAndUpdateDto currencyCreateDto)
        {
            CurrencyValidator validator = new CurrencyValidator();
            var validatorResult = validator.Validate(currencyCreateDto);
            if (!validatorResult.IsValid)
            {
                var baseResponse = new BaseResponse<object, List<ValidationFailure>>
                {
                    result = false,
                    data = "",
                    Errors = validatorResult.Errors,
                    StatusCode = StatusCodes.Status400BadRequest,
                    successMessage = "",
                    errorMessage = ""
                };
                return Ok(baseResponse);
            }
            else
            {

                var result = await  _service.CurrencyService.CreateRecord(currencyCreateDto, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)) );
                var response = new BaseResponse<object, object>
                {
                    data = result,
                    errorMessage = "",
                    successMessage = "Currency created successfully",
                    StatusCode = 200,
                    result = true,
                    Errors = "",
                };

                return Ok(response);
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCurrency(int id, [FromBody] CurrencyForCreationAndUpdateDto currencyCreateDto)
        {

            CurrencyValidator validator = new CurrencyValidator();
            var validatorResult = validator.Validate(currencyCreateDto);
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
                var result = await  _service.CurrencyService.UpdateRecord(id, currencyCreateDto, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)) );
                var response = new BaseResponse<object, object>
                {
                    data = result,
                    result = true,
                    errorMessage = "",
                    successMessage = "Currency updated successfully",
                    StatusCode = 200,
                    Errors = "",

                };
                return Ok(response);
            }
        }


        [HttpPut("{currencyId}/add-banks")]
        public async Task<IActionResult> AddBanksInCurrency (int currencyId, [FromBody] PostBanksToCurrencyDto banks)
        {
            var result = await _service.CurrencyService.PostBanksToCurrency(currencyId, banks, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)) );
            var baseResponse = new BaseResponse<object, object>
            {
                result = result,
                data = "",
                Errors = "",
                StatusCode = 200,
                errorMessage = "Banks were added succesfully for this currency",
                successMessage = ""
            };
            return Ok(baseResponse);
        }


    }
}
