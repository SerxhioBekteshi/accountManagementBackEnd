using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseFeatures
{
    public class PagedListResponse<T>
    {

        public List<DataTableColumn> Columns { get; set; }
        public T Rows { get; set; }
        public List<TotalColumns> Totals { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Key { get; set; }
        public bool hasNext { get; set; }
        public bool hasPrevious { get; set; }

    }
}
