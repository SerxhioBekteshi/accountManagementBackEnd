using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class AutocompleteDto
    {
        public string SearchTerm { get; set; }
        public int Top { get; set; }
    }
}
