using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTC
{
    public class DataTableIcons
    {
        private string name;
        private string icon;

        public DataTableIcons(string name, string icon)
        {
            this.name = name;
            this.icon = icon;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
     
    }
}
