

using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.ResponseFeatures;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;

        public ListController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IServiceManager service)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }


        [HttpGet("categories/get-all")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _service.CategoryService.GetCategoriesAsAList();
            var baseResponse = new BaseResponse<IEnumerable<CategoryListDto>, object>
            {
                result = true,
                data = categories,
                successMessage = "",
                errorMessage = "",
                StatusCode = 200,
                Errors = "",

            };

            return Ok(baseResponse);

        }

        [HttpGet("banks/get-all")]
        public async Task<IActionResult> GetBanks()
        {
            var categories = await _service.BankService.GetBanksAsAList();
            var baseResponse = new BaseResponse<IEnumerable<BankListDto>, object>
            {
                result = true,
                data = categories,
                successMessage = "",
                errorMessage = "",
                StatusCode = 200,
                Errors = "",

            };

            return Ok(baseResponse);

        }

        [HttpPost("managers/autocomplete")]
        public async Task<IActionResult> GetManagers([FromBody] AutocompleteDto autocomplete)
        {
            var managers = await _service.UserService.GetAllManagersAsync(autocomplete);
            var baseResponse = new BaseResponse<IEnumerable<UserDtoManagerList>, object>
            {
                result = true,
                data = managers,
                successMessage = "",
                errorMessage = "",
                StatusCode = 200,
                Errors = "",

            };

            return Ok(baseResponse);

        }
    }
}
