using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.EntityFramework.Queries
{
    public class OrderByItem
    {
        public OrderByItem(string name, SortOrder direction)
        {
            Name = name;
            Direction = direction;
        }
        public string Name { get; private set; }

        public SortOrder Direction { get; private set; }

        public string Generate()
        {
            if (Direction == SortOrder.Ascending)
            {
                return Name;
            }
            return string.Format("{0} desc", Name);
        }
    }
}
