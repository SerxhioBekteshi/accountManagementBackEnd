using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CurrencyBank
    {
        [Key]
        public int Id { get; set; }
        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency? Currency { get; set; }
        public int? BankId { get; set; }
        [ForeignKey("BankId")]
        public BankAccount? BankAccount { get; set; }
    }
}
