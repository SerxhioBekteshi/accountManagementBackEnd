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
    public class SaleTransactionDto
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public int BankAccountId { get; set; }
        public string BankAccountName { get; set; }
        public List<ProductSaleListDto> ProductSale { get; set; }
    }
}
