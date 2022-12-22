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
        public decimal Balance{ get; set; }
        public Boolean IsActive { get; set; }

        [ForeignKey(nameof(User))]
        public int ClientId { get; set; }
        User User { get; set; }

        [ForeignKey(nameof(Bank))]
        public int BankId { get; set; }
        Bank Bank{ get; set; }

    }
}
