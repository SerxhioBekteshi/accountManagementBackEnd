using AccountManagement.Utilities;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    //[Route("api/test/[action]")]
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;
        private readonly IMapper _mapper;

        public DapperController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _dapperRepository.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("CompanyById/{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            try
            {
                var company = await _dapperRepository.GetCompany(id)
;
                if (company == null)
                {
                    return NotFound();
                }
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationAndUpdateDto company)
        {
            try
            {
                var createdCompany = await _dapperRepository.CreateCompany(company);
                return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var dbDcompany = await _dapperRepository.GetCompany(id)
;
            if (dbDcompany is null)
                return NotFound();


            await _dapperRepository.DeleteCompany(id)
;

            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForCreationAndUpdateDto company)
        {
            var dbDcompany = await _dapperRepository.GetCompany(id)
;
            if (dbDcompany is null)
                return NotFound();
            await _dapperRepository.UpdateCompany(id, company);
            return NoContent();

        }

        [HttpGet("ByEmployeeId/{id}")]
        public async Task<IActionResult> GetCompanyForEmployee(Guid id)
        {
            var company = await _dapperRepository.GetCompanyByEmployeeId(id)
;
            if (company is null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet("{id}/MultipleResult")]

        public async Task<IActionResult> GetMultipleResults(Guid id)
        {
            var company = await _dapperRepository.GetMultipleResults(id)
;
            if (company is null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpGet("MultipleMapping")]

        public async Task<IActionResult> GetMultipleMapping()
        {
            var companyies = await _dapperRepository.MultipleMapping();
            return Ok(companyies);
        }

        [HttpGet("BankTransactionById/{bankAccountId}")]
        public async Task<IActionResult> GetBankTransactionsByBankAccount(int bankAccountId)
        {

            var bankTransactions = await _dapperRepository.GetBankTransactionsBasedOnBankAccount(bankAccountId);
            return Ok(bankTransactions);

        }

        [HttpGet("BankAccountsByClientId/{userId}")]
        public async Task<IActionResult> GetActiveAccounts(string userId)
        {

            var bankAccounts = await _dapperRepository.GetActiveAccounts(userId);
            return Ok(bankAccounts);

        }

        [HttpGet("ProductsByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetProductsPerCategory(int categoryId)
        {
            try
            {
                var products = await _dapperRepository.GetProductsPerCategory(categoryId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("UserBankCurrency")]
        public async Task<IActionResult> GetInfo()
        {
            try
            {
                var info = await _dapperRepository.GetGeneralInfo();
                return Ok(info);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}