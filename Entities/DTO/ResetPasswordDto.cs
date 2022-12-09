using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ResetPasswordDto
    {
        public string currentPassword { get; set; }

        public string newPassword { get; set; }


        public string confirmPassword { get; set; }

    }
}
