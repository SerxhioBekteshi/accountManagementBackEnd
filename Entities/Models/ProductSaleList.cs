using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ProductSaleList
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }  
        public Product Product { get; set; }
        public int QuantityToSale { get; set; }

        [ForeignKey(nameof(SaleTransaction))]
        public int SaleTransactionId { get; set; }
        public SaleTransaction SaleTransaction { get; set; }
    }
}
