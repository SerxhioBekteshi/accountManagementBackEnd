using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BankTransactionForCreateDto
    {
        public int BankAccountId { get; set; }  

        public int Action { get; set; }

        public decimal Amount { get; set; }

   
    }
}
