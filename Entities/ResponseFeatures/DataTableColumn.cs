using Entities.DTC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseFeatures
{
    public class DataTableColumn
    {
        public string Field { get; set; }
        public string HeaderName { get; set; }
        public bool Hide { get; set; }
        public bool Hideable { get; set; }
        public string Description { get; set; }
        public bool Sortable { get; set; }
        public bool Editable { get; set; }
        public bool Filterable { get; set; }
        public int MinWidth { get; set; }
        public DataType PropertyType { get; set; }
        public object[] DataTableIcons { get; set; }

    }
}

public enum DataType
{
    Number,
    String,
    DateTime,
    Decimal,
    Boolean,
    Actions,
    Link,
    Custom
}