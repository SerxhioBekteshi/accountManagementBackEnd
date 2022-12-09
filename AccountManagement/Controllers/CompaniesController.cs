using AccountManagement.ModelBinders;
using AccountManagement.Utilities;
using AccountManagement.Validation;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
         
            var result = await _service.CompanyService.GetRecordById(id);
            var baseResponse = new BaseResponse<CompanyDto, object>
            {
                result = true,
                data = result,
                Errors = "",
                StatusCode = StatusCodes.Status200OK,
                successMessage = "",
                errorMessage = "",
            };
            return Ok(baseResponse);
        }

        //[HttpGet("loggedUserGetCompany")]
        //public async Task<IActionResult> GetCompanyPerLoggedUser()
        //{
        //    var company = await _repository.Company.GetAllCompaniesAsync();
        //    if (company == null)
        //    {
        //        _logger.LogInfo($"Company with doesn't exist in the database");
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        var companyDto = _mapper.Map<CompanyDto>(company);
        //        var response = new BaseResponse<CompanyDto, object>
        //        {
        //            data = (CompanyDto)companyDto,
        //            result = true,
        //            errorMessage = "",
        //            successMessage = "",
        //            StatusCode = 200
        //        };
        //        return Ok(response);
        //    }

        //}

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationAndUpdateDto company)
        {

            CompanyValidator validator = new CompanyValidator();
            var validatorResult = validator.Validate(company);
            if (!validatorResult.IsValid)
            {
                var baseResponse = new BaseResponse<object, List<ValidationFailure>>
                {
                    result = false,
                    data = "",
                    Errors = validatorResult.Errors,
                    StatusCode = 400,
                    errorMessage = "",
                    successMessage = ""
                };
                return Ok(baseResponse);
            }
            else
            {
                var result = await _service.CompanyService.CreateRecord(company, int.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

                var baseResponse = new BaseResponse<object, object>
                {
                    result = true,
                    data = result,
                    Errors = "",
                    StatusCode = 200,
                    successMessage = "Company Created Successfully",
                    errorMessage = "",
                };
                return Ok(baseResponse);
            }
        }  

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
         
            var result = await _service.CompanyService.DeleteRecord(id);
            var baseResponse = new BaseResponse<object, object>
            {
                result = result,
                data = "",
                Errors = "",
                StatusCode = 200,
                successMessage = "Company deleted successfully",
                errorMessage = "",
            };
            return Ok(baseResponse);

        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyForCreationAndUpdateDto company)
        {

            CompanyValidator validator = new CompanyValidator();
            var validatorResult = validator.Validate(company);
            if (!validatorResult.IsValid)
            {
                var baseResponse = new BaseResponse<object, List<ValidationFailure>>
                {
                    result = false,
                    data = "",
                    Errors = validatorResult.Errors,
                    StatusCode = 400,
                    errorMessage = "",
                    successMessage = ""
                };
                return Ok(baseResponse);
            }
            else
            {
                var result = await _service.CompanyService.UpdateRecord(id, company, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

                var baseResponse = new BaseResponse<object, object>
                {
                    result = result,
                    data = "",
                    Errors = "",
                    StatusCode = StatusCodes.Status200OK,
                    successMessage = "Company Updated Successfully",
                    errorMessage = "",
                };
                return Ok(baseResponse);
            }
        }



        //[HttpPut("{id}"), Authorize]
        //public async Task<IActionResult> UpdateCompanyCategories(int id, [FromBody] CompanyForCreateCategoriesDto company)
        //{

        //    var result = await _service.CompanyService.UpdateRecord(id, company, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

        //    var baseResponse = new BaseResponse<object, object>
        //    {
        //        result = result,
        //        data = "",
        //        Errors = "",
        //        StatusCode = StatusCodes.Status200OK,
        //        successMessage = "Company Updated Successfully",
        //        errorMessage = "",
        //    };
        //    return Ok(baseResponse);

        //}

        [HttpPost("get-all")]
        public async Task<IActionResult> GetCompaniesByService([FromBody] RequestTableBodyDto request)
        {
            var companiesFromDb = await _service.CompanyService.GetAllRecords(request);

            var baseResponse = new BaseResponse<PagedListResponse<IEnumerable<CompanyDto>>, object>
            {
                result = true,
                data = companiesFromDb,
                successMessage = "",
                errorMessage = "",
                StatusCode = 200,
                Errors = "",

            };

            return Ok(baseResponse);
        }

        [HttpPost("loggedUser/get-all")]
        public async Task<IActionResult> GetCompaniesPerLoggedInManagerByService([FromBody] RequestTableBodyDto request)
        {
            var companiesFromDb = await _service.CompanyService.GetAllCompaniesPerManager(Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)), request);

            var baseResponse = new BaseResponse<PagedListResponse<IEnumerable<CompanyDto>>, object>
            {
                result = true,
                data = companiesFromDb,
                successMessage = "",
                errorMessage = "",
                StatusCode = 200,
                Errors = "",

            };

            return Ok(baseResponse);
        }

        [HttpGet("{companyId}/categories")]

        public async Task<IActionResult> GetCategoriesForCompany(int companyId)
        {
            var result = await _service.CompanyService.GetCategoriesForCompany(companyId, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));
            var baseResponse = new BaseResponse<IEnumerable<CategoryDto>, object>
            {
                result = true,
                data = result,
                Errors = "",
                StatusCode = 200,
                errorMessage = "",
                successMessage = ""
            };
                return Ok(baseResponse);
        }

        [HttpPut("{companyId}/add-categories")]
        public async Task<IActionResult> GetCompaniesPerLoggedInManagerByService(int companyId, [FromBody] PostCategoriesToCompanyDto categories)
        {
            var result = await _service.CompanyService.PostOtherCategoriesForCompany(companyId, categories, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));
            var baseResponse = new BaseResponse<object, object>
            {
                result = result,
                data = "",
                Errors = "",
                StatusCode = 200,
                errorMessage = "Categories were added succesfully for this company",
                successMessage = ""
            };
            return Ok(baseResponse);
        }



        [HttpDelete("category/{companyId}/{categoryId}")]
        public async Task<IActionResult> DeleteRelationCategory(int companyId, int categoryId)
        {
            var result = await _service.CompanyService.DeleteRelationCategory(companyId, categoryId);
            var baseResponse = new BaseResponse<object, object>
            {
                result = result,
                data = "",
                Errors = "",
                StatusCode = 200,
                errorMessage = "",
                successMessage = "Category was deleted succesfully from this company"
            };
            return Ok(baseResponse);
        }

        [HttpDelete("product/{companyId}/{productId}")]
        public async Task<IActionResult> DeleteRelationProduct(int companyId, int productId)
        {
            var result = await _service.CompanyService.DeleteRelationProduct(companyId, productId);
            var baseResponse = new BaseResponse<object, object>
            {
                result = result,
                data = "",
                Errors = "",
                StatusCode = 200,
                errorMessage = "",
                successMessage = "Product was deleted succesfully from this company"
            };
            return Ok(baseResponse);
        }

        [HttpPut("{companyId}/assignManager")]
        public async Task<IActionResult> AssignMangerForCompany(int companyId, [FromBody] CompanyManagerDto manager)
        {
            var result = await _service.CompanyService.AssignManagerForCompany(companyId, manager);
            var baseResponse = new BaseResponse<object, object>
            {
                result = result,
                data = "",
                Errors = "",
                StatusCode = 200,
                errorMessage = "",
                successMessage = "Manager was assigned successfully for this company"
            };
            return Ok(baseResponse);
        }

    }
}
