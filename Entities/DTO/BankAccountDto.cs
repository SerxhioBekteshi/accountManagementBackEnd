using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BankAccountDto
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsActive { get; set; }
        public int? CurrencyId { get; set; }
        public int? ClientId { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByFullName { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public string? ModifiedByFullName { get; set; }
    }
}
