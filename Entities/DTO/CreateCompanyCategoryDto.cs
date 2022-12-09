using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CreateCompanyCategoryDto
    {
        public int? CompanyId { get; set; }
        public int? CategoryId { get; set; }
    }
}
