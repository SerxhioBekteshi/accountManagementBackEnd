using AccountManagement.Utilities;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.ResponseFeatures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public UserController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserDetails ()
        {
            var userEntity = await _repository.User.GetUserDetails(trackChanges: false, ClaimsUtility.ReadCurrentuserId(User.Claims));

            if(userEntity == null )
            {
                _logger.LogInfo("Something went wrong");
                return BadRequest("Something went wrong");
            }

            var userDto = _mapper.Map<UserDto>(userEntity);
            return Ok(userDto);
        }

        [HttpPut("user")]
        public async Task<IActionResult> UpdateUserDetails(UserForUpdateDto user)
        {

            if (user == null)
            {
                _logger.LogError("Invalid UserForUpdateDto");
                return BadRequest();
            }

            var userFromDbEntity = await _repository.User.GetUserDetails(trackChanges: true, ClaimsUtility.ReadCurrentuserId(User.Claims));
            if (userFromDbEntity == null)
            {
                _logger.LogInfo($"User to be updated can not be found");
                return NotFound($"User to be updated can not be found");
            }
            _mapper.Map(user, userFromDbEntity);
            await _repository.SaveAsync();

            var response = new BaseResponse<String, object>
            {
                data = "",
                result = true,
                errorMessage = "",
                successMessage = "User details were updated successfully",
                StatusCode = 200
            };
            return Ok(response);
        }
    }
}
