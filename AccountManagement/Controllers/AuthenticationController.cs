using AccountManagement.Utilities;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Entities.ResponseFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IMailService _mailService;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IRepositoryManager _repository;
        private readonly IServiceManager _service;

        public AuthenticationController(UserManager<User> userManager, RoleManager<Roles> roleManager, ILoggerManager logger, IMapper mapper, IAuthenticationManager authManager, IMailService mailService, IRepositoryManager repository, IServiceManager service)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _roleManager = roleManager;
            _authManager = authManager;
            _mailService = mailService;
            _repository = repository;
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);

            if (userForRegistrationDto.Role.Equals(""))
            {

                var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }
                await _userManager.AddToRoleAsync(user, userForRegistrationDto.Role);
                await _repository.SaveAsync();
            }
            else
            {
                if (!await _roleManager.RoleExistsAsync(userForRegistrationDto.Role))
                {
                    _logger.LogError("The role you are assigning does not exist in the database");
                    return BadRequest("The role you are assigning does not exist in the database");
                }
                else
                {
                    if (userForRegistrationDto.CompanyId == 0)
                    {
                        _logger.LogInfo("SOMETHING WENT WRONG");
                        return BadRequest("SOMETHING WENT WRONG");
                    }
                    else
                    {
                        var company = await _service.CompanyService.GetRecordById((int) userForRegistrationDto.CompanyId);
                        if (company == null)
                        {
                            _logger.LogInfo("The company you are trying to create a manager account does not exist in the database");
                            return BadRequest("The company you are trying to create a manager account does not exist in the database");
                        }
                        var companyEntity = _mapper.Map<Company>(company);

                        var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

                        companyEntity.ManagerAccountActivated = "Registered";
                        companyEntity.ManagerId = user.Id;
                        companyEntity.CreatedBy =  Int32.Parse(ClaimsUtility.ReadCurrentuserId(User.Claims));
                        companyEntity.DateCreated = DateTime.Now;

                         _repository.Company.UpdateCompany(companyEntity);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.TryAddModelError(error.Code, error.Description);
                            }

                            return BadRequest(ModelState);
                        }
                        await _repository.SaveAsync();
                        await _userManager.AddToRoleAsync(user, userForRegistrationDto.Role);

                    }
                }
            }

            //await _mailService.SendWelcomeEmailAsync(userForRegistrationDto.Email, userForRegistrationDto.UserName);
            var response = new BaseResponse<String, object>
            {
                data = "",
                result = true,
                errorMessage = "",
                successMessage = "User registered successfully",
                StatusCode = 201
            };
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong email or password.");
                return Unauthorized();
            }
            //return Ok(new { Token = await _authManager.CreateToken() });

            var tokenDto = await _authManager.CreateTokenWithRefreshToken(populateExp: true);
            return Ok(tokenDto);

        }


        [HttpPut("Change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto == null)
            {
                _logger.LogError("Change passwordDto can not be null");
                return BadRequest("Change passwordDto can not be null");
            }

            if (changePasswordDto.newPassword != changePasswordDto.confirmNewPassword)
            {
                var response = new BaseResponse<String, object>
                {
                    data = "",
                    result = true,
                    errorMessage = "New Password and Confirm New Password are not the same ",
                    successMessage = "",
                    StatusCode = 200
                };
                return Ok(response);
            }
            if (!await _authManager.CheckCurrentPassword(changePasswordDto, ClaimsUtility.ReadCurrentuserId(User.Claims)))
            {
                _logger.LogWarn("Current password is wrong");
                return BadRequest("Current password is wrong");
            }

            await _authManager.ChangePassword(changePasswordDto, ClaimsUtility.ReadCurrentuserId(User.Claims));
            //await _authManager.ChangePassword() 
            return Ok("Password was change successfully");

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await
            _authManager.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }
    }
}
