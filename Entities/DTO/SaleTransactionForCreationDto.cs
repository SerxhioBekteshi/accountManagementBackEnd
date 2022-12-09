using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class SaleTransactionForCreationDto
    {
        public decimal Total { get; set; }
        public int BankAccountId { get; set; }
        public List<ProductSaleListForCreationDto> ProductSale { get; set; }
    }
}
