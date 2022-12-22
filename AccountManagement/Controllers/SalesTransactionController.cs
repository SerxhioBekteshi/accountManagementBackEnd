using AccountManagement.Utilities;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.ResponseFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SalesTransactionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDapperRepository _dapperRepository;


        public SalesTransactionController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dapperRepository = dapperRepository;
        }

        //[HttpPost]

        //public async Task<IActionResult> MakeSale([FromBody] SaleTransactionForCreationDto saleForTransactionDto)
        //{
        //    if (saleForTransactionDto == null)
        //    {
        //        _logger.LogError("Invalid saleForTransactionDto");
        //        return BadRequest();
        //    }

        //    var bankAccountEntity = await _repository.BankAccount.GetBankAccountAsync(saleForTransactionDto.BankAccountId);
        //    if (bankAccountEntity == null)
        //    {
        //        _logger.LogInfo($"The bank account with {saleForTransactionDto.BankAccountId} does not exist in the database for the current user");
        //        return NotFound();
        //    }

        //    foreach (var saleBeingMade in saleForTransactionDto.ProductSale)
        //    {
        //        var productFromDb = await _repository.Product.GetProductAsync(saleBeingMade.ProductId);
        //        var productDto = _mapper.Map<ProductDto>(productFromDb);

        //        if(saleBeingMade.QuantityToSale > productDto.Sasia)
        //        {
        //            var response2 = new BaseResponse<String, object>
        //            { 
        //                data = "",
        //                errorMessage = $"The quantity of the product with {saleBeingMade.ProductId} you want to buy is not in the storage",
        //                successMessage = "",
        //                StatusCode = 400,
        //                result = false
        //            };
        //            return Ok(response2);
        //            //_logger.LogError($"The quantity of the product with {saleBeingMade.ProductId} you want to buy is not in the storage");
        //            //return BadRequest($"The quantity of the product with {saleBeingMade.ProductId} you want to buy is not in the storage");
        //        }
        //        //productFromDb.Sasia -= saleBeingMade.QuantityToSale;
        //        _repository.Product.UpdateProduct(productFromDb);
        //        await _repository.SaveAsync();
            
        //    }
         
        //    var SaleTransactionEntity = _mapper.Map<SaleTransaction>(saleForTransactionDto);

        //    foreach (var saleDone in SaleTransactionEntity.ProductSale)
        //    {
        //        var productFromDb = await _repository.Product.GetProductAsync(saleDone.ProductId);
        //        saleDone.ProductName = productFromDb.Name;
        //    }

        //    SaleTransactionEntity.bankAccountName = bankAccountEntity.Name; //ESHTE SHTUAR EMRI I BANKES
        //    _repository.SalesTransaction.CreateSaleTransaction(SaleTransactionEntity);
        //    await _repository.SaveAsync();

        //    var response = new BaseResponse<String, object>
        //    {
        //        data = "",
        //        result = true,
        //        errorMessage = "",
        //        successMessage = "Sale was made successfully",
        //        StatusCode = 200
        //    };
        //    return Ok(response);
        //}


        //[HttpGet("get-all/loggedInUser")]
        //public async Task<IActionResult> GetSalesPerUserLoggedIn()
        //{
        //    var bankAccountsFromDb = await _repository.BankAccount.GetAllBankAccountsAsync(Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));
        //    var saleTransactionDictionary = new Dictionary<int, SaleTransactionDto>();

        //    foreach (var bank in bankAccountsFromDb)
        //    {
        //        var saleTransactionEntityFromDb = await _repository.SalesTransaction.GetAllPerUser(bank.Id, trackChanges: false);
        //        var salesDto = _mapper.Map<IEnumerable<SaleTransactionDto>>(saleTransactionEntityFromDb);

        //        foreach (var ss in salesDto)
        //        {
        //            saleTransactionDictionary.Add(ss.Id, ss);

        //        }
        //    }
        //    var salesPerUser = saleTransactionDictionary.Values.ToList();
     
        //    foreach (var sales in salesPerUser)
        //    {
        //        var productsPerSale = await _repository.ProductSaleList.GetProductSaleListPerSale(sales.Id, trackChanges: false);
        //        var productsPerSaleDto = _mapper.Map<IEnumerable<ProductSaleListDto>>(productsPerSale);
        //        sales.ProductSale = (List<ProductSaleListDto>)productsPerSaleDto;
        //    }
        //    var response = new BaseResponse<IEnumerable<SaleTransactionDto>, object>
        //    {
        //        data = salesPerUser,
        //        result = true,
        //        errorMessage = "",
        //        successMessage = "",
        //        StatusCode = 200
        //    };

        //    return Ok(response);
        //}

        //[HttpGet("get-all")]
        //public async Task<IActionResult> GetAllSales()
        //{

        //    var saleTransactionEntityFromDb = await _repository.SalesTransaction.GetAll(trackChanges: false);
        //    if (saleTransactionEntityFromDb == null)
        //    {
        //        _logger.LogInfo("NO DATA");
        //        var response2 = new BaseResponse<String, object>
        //        {
        //            data = "",
        //            result = true,
        //            errorMessage = "There are no data in the database",
        //            successMessage = "",
        //            StatusCode = 200
        //        };
        //        return Ok(response2);

        //    }

        //    var salesDto = _mapper.Map<IEnumerable<SaleTransactionDto>>(saleTransactionEntityFromDb);

        //    foreach (var sales in salesDto)
        //    {
        //        var productsPerSale = await _repository.ProductSaleList.GetProductSaleListPerSale(sales.Id, trackChanges: false);
        //        var productsPerSaleDto = _mapper.Map<IEnumerable<ProductSaleListDto>>(productsPerSale);
        //        sales.ProductSale = (List<ProductSaleListDto>)productsPerSaleDto;
        //    }
        //    var response = new BaseResponse<IEnumerable<SaleTransactionDto>, object>
        //    {
        //        data = salesDto,
        //        result = true,
        //        errorMessage = "",
        //        successMessage = "",
        //        StatusCode = 200
        //    };

        //    return Ok(response);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetSale(int id)
        //{
        //    var saleTransactionEntityFromDb = await _repository.SalesTransaction.GetSale(id, trackChanges: false);

        //    if (saleTransactionEntityFromDb == null)
        //    {
        //        _logger.LogInfo("NO DATA");
        //        var response2 = new BaseResponse<String, object>
        //        {
        //            data = "",
        //            result = true,
        //            errorMessage = "There are no data in the database",
        //            successMessage = "",
        //            StatusCode = 200
        //        };
        //        return Ok(response2);

        //    }
        //    var salesDto = _mapper.Map<SaleTransactionDto>(saleTransactionEntityFromDb);
        
        //    var productsPerSale = await _repository.ProductSaleList.GetProductSaleListPerSale(id, trackChanges: false);
        //    var productsPerSaleDto = _mapper.Map<IEnumerable<ProductSaleListDto>>(productsPerSale);
        //    salesDto.ProductSale = (List<ProductSaleListDto>)productsPerSaleDto;
           
        //    var response = new BaseResponse<SaleTransactionDto, object>
        //    {
        //        data = salesDto,
        //        result = true,
        //        errorMessage = "",
        //        successMessage = "",
        //        StatusCode = 200
        //    };

        //    return Ok(response);
        //}

        //[HttpGet("WithDapper")]
        //public async Task<IActionResult> GetSalesWithProducts()
        //{
        //    var sales = await _dapperRepository.GetSalesAndRespectiveProducts();

        //    return Ok(sales);
        //}

    }
}
