using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }
        public int RoleId { get; set; }
    }
}
