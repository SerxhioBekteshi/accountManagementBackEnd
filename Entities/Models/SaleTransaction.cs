using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SaleTransaction
    {
        [Key]
        public int Id { get; set; }
        public decimal Total { get; set; }

        [ForeignKey(nameof(BankAccount))]
        public string bankAccountName { get; set; }
        public int BankAccountId { get; set; }
        BankAccount BankAccount { get; set; }
        public List<ProductSaleList> ProductSale { get; set; }

    }
}
