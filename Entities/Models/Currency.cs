using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Currency : BaseCreatedAndModified
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal ExhangeRate  { get; set; }
        public List<CurrencyBank>? CurrencyBankAccount { get; set; }
    }
}
