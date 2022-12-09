using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CompanyProductCreateUpdateDto
    {
        public int? CompanyId { get; set; }
        public int? ProductId { get; set; }
    }
}
