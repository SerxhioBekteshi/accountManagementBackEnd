using AutoMapper;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AccountManagement.ModelBinders;
using Entities.Models;
using System.Linq;
using System.Threading.Tasks;
using Entities.RequestFeatures;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Entities.ResponseFeatures;
using Entities.DTC;
using NLog.Filters;
using Repository;
using SendGrid.Helpers.Errors.Model;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using AccountManagement.Utilities;
using AccountManagement.Validation;
using FluentValidation.Results;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/employess")]
    [ApiController]
    public class EmployessController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<EmployeeDto> _dataShaper;

        private readonly IServiceManager _service;

        public EmployessController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<EmployeeDto> dataShaper, IServiceManager service)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _service = service;
        }

        ////PUNON
        //[HttpGet("get-all"), Authorize]
        //public async Task <IActionResult> GetEmployees()
        //{
      
        //    var employeesFromDb = await _repository.Employee.GetAllEmployeesAsync();
        //    var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
        //    return Ok(employeesDto);
        //}

        [HttpGet("{companyId}/get-all")]
        public async Task <IActionResult> GetEmployeesForCompany(int companyId, [FromQuery] EmployeeParameters employeeParameters)
        {

            //if (!employeeParameters.ValidAgeRange)
            //    return BadRequest("Max age can not be less than min age");

            var company = await _repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeesFromDb = await _repository.Employee.GetEmployeesPerCompanyAsync(companyId, employeeParameters);
                Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(employeesFromDb.MetaData));

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
                return Ok(_dataShaper.ShapeData(employeesDto, employeeParameters.Fields));
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetEmployee(int id)
        {
            //var employeeDb = await _repository.Employee.GetEmployeeAsync(id);
            //if (employeeDb == null)
            //{
            //    _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            //    return NotFound();
            //}
            //var employee = _mapper.Map<EmployeeDto>(employeeDb);
            //return Ok(employee);


            var result = await _service.EmployeeService.GetRecordById(id);
            var baseResponse = new BaseResponse<object, object>
            {
                result = true,
                data = result,
                Errors = "",
                StatusCode = 200,
                successMessage = "",
                errorMessage = "",
            };
            return Ok(baseResponse);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task <IActionResult> DeleteEmployee(int id)
        {
            //var employeeForCompany = await _repository.Employee.GetEmployeeAsync(id);
            //if (employeeForCompany == null)
            //{
            //    _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            //    return NotFound();
            //}
            //_repository.Employee.DeleteEmployee(employeeForCompany);
            //await _repository.SaveAsync();
            //return Ok("Fshirja u kry me sukses");

            var result = await _service.EmployeeService.DeleteRecord(id);
            var baseResponse = new BaseResponse<object, object>
            {
                result = result,
                data = "",
                Errors = "",
                StatusCode = 200,
                successMessage = "Employee deleted successfully",
                errorMessage = "",
            };
            return Ok(baseResponse);
        }


        [HttpPut("{id}"), Authorize]
        public async Task <IActionResult> UpdateEmployee(int id, [FromBody] EmployeeForCreationAndUpdateDto employee)
        {
            //if (employee == null)
            //{
            //    _logger.LogError("EmployeeForUpdateDto object sent from client is null.");
            //    return BadRequest("EmployeeForUpdateDto object is null");
            //}
            //if (!ModelState.IsValid)
            //{
            //    _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
            //    return UnprocessableEntity("EmployeeForCreationDto objcet is null");
            //}
            //var employeeEntity = await  _repository.Employee.GetEmployeeAsync( id );
            //if (employeeEntity == null)
            //{
            //    _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            //    return NotFound();
            //}
            //_mapper.Map(employee, employeeEntity);
            //await _repository.SaveAsync();
            //return Ok("Editimi u krye me sukses");


            EmployeeValidation validator = new EmployeeValidation();
            var validatorResult = validator.Validate(employee);
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
                var result = await _service.EmployeeService.UpdateRecord(id, employee, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

                var baseResponse = new BaseResponse<object, object>
                {
                    result = result,
                    data = "",
                    Errors = "",
                    StatusCode = 200,
                    successMessage = "Employee Updated Successfully",
                    errorMessage = "",
                };
                return Ok(baseResponse);
            }
        }


        [HttpPost("{companyId}"), Authorize]
        public async Task <IActionResult> CreateEmployee([FromBody] EmployeeForCreationAndUpdateDto employee, int companyId)
        {
            //if (employee == null)
            //{
            //    _logger.LogError("EmployeeForCreationDto object sent from client is null.");
            //    return BadRequest("EmployeeForCreationDto object is null");
            //}  

            //var employeeEntity = _mapper.Map<Employee>(employee);

            //var companyFromDb = await _repository.Company.GetCompanyByManagerAsync(Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));
            //employeeEntity.CompanyId = companyFromDb.Id;

            //_repository.Employee.CreateEmployee(employeeEntity);
            //await _repository.SaveAsync();

            //var baseResponse = new BaseResponse<String, object>
            //{
            //    result = true,
            //    data = "",
            //    successMessage = "You added an employee successfully",
            //    errorMessage = "",
            //    StatusCode = 200,
            //};

            //return Ok(baseResponse);

            EmployeeValidation validator = new EmployeeValidation();
            var validatorResult = validator.Validate(employee);
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
                var result = await _service.EmployeeService.CreateRecord(employee, int.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)), companyId);

                var baseResponse = new BaseResponse<object, object>
                {
                    result = true,
                    data = result,
                    Errors = "",
                    StatusCode = 200,
                    successMessage = "Employee Created Successfully for this company",
                    errorMessage = "",
                };
                return Ok(baseResponse);
            }
        }


        //[HttpPatch("{id}"), Authorize]
        //public async Task <IActionResult> PartiallyUpdateEmployeeForCompany(int id,[FromBody] JsonPatchDocument<EmployeeForCreationAndUpdateDto> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        _logger.LogError("patchDoc object sent from client is null.");
        //        return BadRequest("patchDoc object is null");
        //    }
        //    var employeeEntity = await _repository.Employee.GetEmployeeAsync( id);
        //    if (employeeEntity == null)
        //    {
        //        _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
        //        return NotFound();
        //    }
        //    var employeeToPatch = _mapper.Map<EmployeeForCreationAndUpdateDto>(employeeEntity);
        //    patchDoc.ApplyTo(employeeToPatch);
        //    _mapper.Map(employeeToPatch, employeeEntity);
        //    await _repository.SaveAsync();
        //    return NoContent();
        //}


        [HttpPost("{companyId}/get-all")]
        public async Task<IActionResult> GetEmployessByService([FromBody] RequestTableBodyDto request, int companyId)
        {
            var companiesFromDb = await _service.EmployeeService.GetAllRecords( ClaimsUtility.ReadCurrentuserId(User.Claims), companyId, request);

            var baseResponse = new BaseResponse<PagedListResponse<IEnumerable<EmployeeDto>>, object>
            {
                result = true,
                data = companiesFromDb,
                successMessage = "",
                errorMessage = "",
                StatusCode = 200,
            };

            return Ok(baseResponse);
        }
    }
}
