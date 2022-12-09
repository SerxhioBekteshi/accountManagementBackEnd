using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BankTransactionDto
    {
        public int Id { get; set; }

        public int BankAccountId { get; set; }

        public int Action { get; set; }

        public int Amount { get; set; }

        public Boolean IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
