using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CompanyDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? ManagerAccountActivated { get; set; }
        public int? ManagerId { get; set; } 
        public string? ManagerFullName { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByFullName { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public string? ModifiedByFullName { get; set; }
        public List<int?>? CategoryIds { get; set; }


    }
}
