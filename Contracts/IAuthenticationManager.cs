using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
        Task<TokenDto> CreateTokenWithRefreshToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task<bool> CheckCurrentPassword(ChangePasswordDto changePasswordDto, string userId);
        Task ChangePassword(ChangePasswordDto changePasswordDto, string userId);

    }
}
