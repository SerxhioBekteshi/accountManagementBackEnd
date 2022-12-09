using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CurrencyBankCreateUpdateDto
    {
        public int? BankId { get; set; }
        public int? CurrencyId { get; set; }
    }
}
