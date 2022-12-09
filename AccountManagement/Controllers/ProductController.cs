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
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public ProductController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IServiceManager serviceManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _service = serviceManager;
        }

        [HttpPost("get-all"),Authorize]
        [Authorize]
        public async Task<IActionResult> GetProducts([FromBody] RequestTableBodyDto request)
        {

            //var products = await _repository.Product.GetAllProductsAsync();

            //var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            //var response = new BaseResponse<IEnumerable<ProductDto>, object>
            //{
            //    data = (IEnumerable<ProductDto>)productsDto,
            //    result = true,
            //    errorMessage = "",
            //    successMessage = "",
            //    StatusCode = 200
            //};

            //return Ok(response);

            var productsFromDb = await _service.ProductService.GetAllRecords(request);

            var baseResponse = new BaseResponse<PagedListResponse<IEnumerable<ProductDto>>, object>
            {
                result = true,
                data = productsFromDb,
                successMessage = "",
                errorMessage = "",
                StatusCode = 200,
                Errors = "",

            };

            return Ok(baseResponse);

        }


        [HttpGet("{id}"), Authorize]
        [Authorize]
        public async Task<IActionResult> GetProduct(int id)
        {

            //var product = await _repository.Product.GetProductAsync(id);
            //if (product == null)
            //{
            //    _logger.LogInfo($"The Product with {id} does not exist in the database");
            //    return NotFound();
            //}

            //var productDto = _mapper.Map<ProductDto>(product);
            //var response = new BaseResponse<ProductDto, object>
            //{
            //    data = (ProductDto)productDto,
            //    result = true,
            //    errorMessage = "",
            //    successMessage = "",
            //    StatusCode = 200
            //};

            //return Ok(response);

            var result = await _service.ProductService.GetRecordById(id);
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

        [HttpGet("{categoryId}/get-all"), Authorize]
        [Authorize]
        public async Task<IActionResult> GetCategoryProducts(int categoryId)
        {

            var category = await _repository.Category.GetCategory(categoryId);
            if (category == null)
            {
                _logger.LogInfo($"The Category with {categoryId} does not exist in the database");
                return NotFound();
            }

            var products = await _repository.Product.GetAllProductsAsyncPerCategory(categoryId);
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            var response = new BaseResponse<IEnumerable<ProductDto>, object>
            {
                data = (IEnumerable<ProductDto>)productDto,
                result = true,
                errorMessage = "",
                successMessage = "",
                StatusCode = 200
            };

            return Ok(response);

        }

        [HttpDelete("{id}"), Authorize]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            //var product = await _repository.Product.GetProductAsync(id);

            //if (product == null)
            //{
            //    var response2 = new BaseResponse<String, object>
            //    {
            //        data = "",
            //        result = true,
            //        errorMessage = "The prodcut u want to delete does not exist in the database",
            //        successMessage = "",
            //        StatusCode = 200
            //    };
            //    return BadRequest(response2);
            //}

            //_repository.Product.DeleteProduct(product);
            //await _repository.SaveAsync();
            //var response = new BaseResponse<String, object>
            //{
            //    data = "",
            //    result = true,
            //    errorMessage = "",
            //    successMessage = "The prodcut was deleted successfully",
            //    StatusCode = 200
            //};

            //return Ok(response);

            var result = await _service.ProductService.DeleteRecord(id);
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

        [HttpPut("{id:int}"), Authorize]
        [Authorize]
        public async Task<IActionResult> UpdateProduct (int id, [FromBody] ProductForCreationAndUpdateDto product)
        {

            //if (product == null)
            //{
            //    _logger.LogError("ProductForUpdateDto object sent from client is null.");
            //    return BadRequest("ProductForUpdateDto object is null");
            //}

            //var productEntity = await _repository.Product.GetProductAsync(id);
            //if (productEntity == null)
            //{
            //    _logger.LogError($"This product with {id} does not exist in the database");
            //    return NotFound();
            //}
            //productEntity.DateModified = DateTime.Now;
            //_mapper.Map(product, productEntity);
            //await _repository.SaveAsync();
            //var response = new BaseResponse<String, object>
            //{ 
            //    data = "",
            //    result =true,
            //    errorMessage = "",
            //    successMessage = "Editimi u kry me sukses",
            //    StatusCode = 200
            //};

            //return Ok(response);

            ProductValidation validator = new ProductValidation();
            var validatorResult = validator.Validate(product);
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
                var result = await _service.ProductService.UpdateRecord(id, product, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationAndUpdateDto product)
        {
            //if (product == null)
            //{
            //    _logger.LogError("ProductForCreateDto object sent from client is null.");
            //    return BadRequest("ProductForCreateDto object is null");
            //}

            //var productEntity = _mapper.Map<Product>(product);
            //productEntity.DateCreated = DateTime.Now;

            //_repository.Product.CreateProduct(productEntity);
            //await _repository.SaveAsync();
            //var response = new BaseResponse<String, object>
            //{
            //    data = "",
            //    result = true,
            //    errorMessage = "",
            //    successMessage = "The prodcut was created successfully",
            //    StatusCode = 200
            //};
            //return Ok(response);

            ProductValidation validator = new ProductValidation();
            var validatorResult = validator.Validate(product);
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

                var result = await _service.ProductService.CreateRecord(product, Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims)));

                var baseResponse = new BaseResponse<object, object>
                {
                    result = true,
                    data = result,
                    errorMessage = "",
                    successMessage = "Product Created successfully",
                    StatusCode = 200,
                    Errors = ""
                };
                return Ok(baseResponse);
            }
        }

        [HttpPut("{id}/uploadImage"), Authorize]
        public async Task<IActionResult> UploadProductPhoto(int id, [FromForm] ProductFileUploadDto productFile)
        {
            var product = await _repository.Product.GetProductAsync(id);
            if (product == null)
            {
                _logger.LogError($"This product with {id} does not exist in the database");
                return NotFound();
            }
            var result = await _repository.Product.UploadImage(id, productFile);
            await _repository.SaveAsync();  
            var response = new BaseResponse<String, object>
            {
                data = "",
                result = result,
                errorMessage = "",
                successMessage = "The photo was updated successfully",
                StatusCode = 200
            };
            return Ok(response);
        }

    }
}
