using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? ShortDescription { get; set; } 
        public string? LongDescription { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByFullName { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public string? ModifiedByFullName { get; set; }
        public int? Sasia { get; set; }
    }
}
