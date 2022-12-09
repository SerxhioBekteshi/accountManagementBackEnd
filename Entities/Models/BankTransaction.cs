using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BankTransaction : BaseCreatedAndModified
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Action { get; set; }
        public Boolean IsActive { get; set; }

        [ForeignKey(nameof(BankAccount))]
        public int BankAccountId { get; set; }
        BankAccount BankAccount { get; set; }
    }
}
