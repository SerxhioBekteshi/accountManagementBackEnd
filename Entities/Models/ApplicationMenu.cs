using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationMenu
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Route { get; set; }

        public string Icon { get; set; }

        [ForeignKey(nameof(Roles))]
        public int RoleId { get; set; } 
        public Roles Roles { get; set; }
    }
}
