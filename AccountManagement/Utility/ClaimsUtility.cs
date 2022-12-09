using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AccountManagement.Utilities
{
    public static class ClaimsUtility
    {
        public static string ReadCurrentuserId(IEnumerable<Claim> claims)
        {
            string userId = claims.First(claims => claims.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        public static int ReadCurrentuserRole(IEnumerable<Claim> claims)
        {
            string role = claims.First(claims => claims.Type == ClaimTypes.Role).Value;
            if (role == "Manager")
                return 1;
            else if (role == "Admin")
                return 2;
            else 
            return 3;
        }
    }
}
