using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.ResponseFeatures;
using Microsoft.AspNetCore.Mvc;
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
        public CurrencyController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCurrencies ()
        {
            var currenciesFromDb = await _repository.Currency.GetAllCurrenciesAsync(trackChanges: false);
            var currenciesDto = _mapper.Map<IEnumerable<CurrencyDto>>(currenciesFromDb);

            var response = new BaseResponse<IEnumerable<CurrencyDto>, object>
            { 
                data = currenciesDto, 
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
            var currencyFromDb = await _repository.Currency.GetCurrencyAsync(id, trackChanges: false);
            if(currencyFromDb == null)
            {
                _logger.LogInfo($"The currency with {id} does not exist in the database");
                return NotFound($"The currency with {id} does not exist in the database");
            }

            var currencyDto = _mapper.Map<CurrencyDto>(currencyFromDb);
            var response = new BaseResponse<CurrencyDto, object>
            {
                data = currencyDto,
                errorMessage = "",
                successMessage = "",
                StatusCode = 200,
                result = true,
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCurreny (int id)
        {
            var currencyFromDb = await _repository.Currency.GetCurrencyAsync(id, trackChanges: false);
            if(currencyFromDb == null)
            {
                _logger.LogError("You can not delete a currency that does not exist in the database");
                return BadRequest("You can not delete a currency that does not exist in the database");
            }

            _repository.Currency.DeleteCurrency(currencyFromDb);
            await _repository.SaveAsync();

            var response = new BaseResponse<String, object>
            {
                data = { },
                errorMessage = "",
                successMessage = "Currency deleted successfully",
                StatusCode = 200,
                result = true,
            };

            return Ok(response);

        }

        [HttpPost]

        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyCreateDto currencyCreateDto)
        {
            if(currencyCreateDto == null)
            {
                _logger.LogError("Currency For Creation Dto should not be null");
                return BadRequest("Currency For Creation Dto should not be null");
            }

            var currencyEntity = _mapper.Map<Currency>(currencyCreateDto);
            currencyEntity.DateCreated = DateTime.Now;
            _repository.Currency.CreateCurrency(currencyEntity);
            await _repository.SaveAsync();

            var response = new BaseResponse<String, object>
            {
                data = { },
                errorMessage = "",
                successMessage = "Currency created successfully",
                StatusCode = 200,
                result = true,
            };

            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCompanu(int id, [FromBody] CurrencyCreateDto currencyCreateDto)
        {
            if (currencyCreateDto == null)
            {
                _logger.LogError("Currency For Creation Dto should not be null");
                return BadRequest("Currency For Creation Dto should not be null");
            }

            var currencyFromDb = await _repository.Currency.GetCurrencyAsync(id, trackChanges: true);
            if (currencyFromDb == null)
            {
                _logger.LogError("You can not delete a currency that does not exist in the database");
                return BadRequest("You can not delete a currency that does not exist in the database");
            }
            currencyFromDb.DateModified = DateTime.Now;
            _mapper.Map(currencyCreateDto, currencyFromDb);
            await _repository.SaveAsync();
            var response = new BaseResponse<String, object>
            {
                data = "",
                result = true,
                errorMessage = "",
                successMessage = "Currency updated successfully",
                StatusCode = 200
            };
            return Ok(response);

        }

    }
}
