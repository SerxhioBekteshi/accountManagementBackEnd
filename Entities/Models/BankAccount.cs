using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BankAccount : BaseCreatedAndModified
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Balance{ get; set; }
        public Boolean IsActive { get; set; }

        [ForeignKey(nameof(User))]
        public int ClientId { get; set; }
        User User { get; set; }
        public List<CurrencyBank>? CurrencyBankAccount { get; set; }


    }
}
