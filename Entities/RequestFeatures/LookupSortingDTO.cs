using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class LookupSortingDTO
    {
        public string ColumnName { get; set; }
        public LookupSortingDirection Direction { get; set; }
    }
}


public enum LookupSortingDirection
{
    Asc,
    Desc
}
