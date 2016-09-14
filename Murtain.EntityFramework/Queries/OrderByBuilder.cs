using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.Extensions;

namespace Murtain.EntityFramework.Queries
{
    public class OrderByBuilder
    {
        private List<OrderByItem> items { get; set; }
        public OrderByBuilder()
        {
            items = new List<OrderByItem>();
        }

        public string Generate()
        {
            return items.Select(t => t.Generate()).ToList().JoinAsString(",");
        }
        public void Add(string name, bool desc = false)
        {
            if (desc == false)
            {
                items.Add(new OrderByItem(name, SortOrder.Ascending));
            }
            else
            {
                items.Add(new OrderByItem(name, SortOrder.Descending));
            }
        }
        public void Add(string name, SortOrder direction)
        {
            items.Add(new OrderByItem(name, direction));
        }
    }
}
