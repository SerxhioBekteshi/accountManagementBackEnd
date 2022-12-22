using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BankAccountCreateUpdateDto
    {

        public int? BankId { get; set; }
        public int? ClientId { get; set; }
        public bool? isActive { get; set; }
        public decimal balance { get; set; }
    }
}
