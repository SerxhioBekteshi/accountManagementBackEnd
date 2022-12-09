using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ChangePasswordDto
    {

        public string CurrentPassword { get; set; }

        public string newPassword { get; set; }

        public string confirmNewPassword { get; set; }
    }
}