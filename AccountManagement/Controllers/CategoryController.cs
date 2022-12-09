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
    public class CategoryController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;
        public CategoryController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("get-all"),Authorize]
        public async Task<IActionResult> GetCategories([FromBody] RequestTableBodyDto request)
        {
            var categories = await _service.CategoryService.GetAllRecords(request);

            if (categories == null)
            {
                _logger.LogInfo($"SOMETHING WENT WRONG");
                return BadRequest();

            }
            var baseResponse = new BaseResponse<PagedListResponse<IEnumerable<CategoryDto>>, object>
            {
                data = categories,
                result = true,
                errorMessage = "",
                successMessage = "",
                StatusCode = 200,
                Errors = ""
            };

            return Ok(baseResponse);
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetCategoryById (int id)
        {

            var result = await _service.CategoryService.GetRecordById(id);
            var baseResponse = new BaseResponse<CategoryDto, object>
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
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationAndUpdateDto category)
        {
         
            CategoryValidation validator = new CategoryValidation();
            var validatorResult = validator.Validate(category);
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

                var result = await _service.CategoryService.CreateRecord(category, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

                var baseResponse = new BaseResponse<object, object>
                {
                    result = true,
                    data = result,
                    errorMessage = "",
                    successMessage = "Created successfully",
                    StatusCode = 200,
                    Errors = ""
                };
                return Ok(baseResponse);
            }

        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryForCreationAndUpdateDto category)
        {
          
            CategoryValidation validator = new CategoryValidation();
            var validatorResult = validator.Validate(category);
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
                var result = await _service.CategoryService.UpdateRecord(id, category, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _service.CategoryService.DeleteRecord(id);
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
