using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ProductSaleListDto
    {
        public int Id { get; set; }
        public int QuantityToSale { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int SaleTransactionId { get; set; }

    }
}
