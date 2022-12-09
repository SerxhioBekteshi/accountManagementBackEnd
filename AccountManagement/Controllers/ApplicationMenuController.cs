using AccountManagement.Utilities;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.ResponseFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationMenuController: ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public ApplicationMenuController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("get-all")]

        public async Task<IActionResult> GetMenu ()
        {
            var menu = await _repository.ApplicationMenu.GetMenu(trackChanges: false, ClaimsUtility.ReadCurrentuserRole(User.Claims));

            if(menu == null)
            {
                _logger.LogInfo("No Menu to display");
                return BadRequest();
            }
            var menuDto = _mapper.Map<IEnumerable<MenuDto>>(menu);
            var response = new BaseResponse<IEnumerable<MenuDto>, object>
            {
                data = menuDto,
                successMessage = "",
                errorMessage = "",
                result = true,
                StatusCode = 200,
            };

            return Ok(response);
        }

    }
}
