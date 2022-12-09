
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	public class Company : BaseCreatedAndModified
    {

        [Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Country { get; set; }
        public string? ManagerAccountActivated { get; set; }
        public List<Employee>? Employees { get; set; }
        public int? ManagerId { get; set; }

        [ForeignKey(nameof(User))]
        User? User { get; set; }
        public List<CompanyCategory>? CompanyCategory { get; set; }
        public List<CompanyProduct>? CompanyProduct { get; set; }

    }
}
