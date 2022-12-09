using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ProductForCreationAndUpdateDto
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int? Sasia { get; set; }
    }
}
