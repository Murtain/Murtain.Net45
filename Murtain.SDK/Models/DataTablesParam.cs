using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murtain.SDK.Models
{
    public class DataTablesParam<TEntity> where TEntity : class, new()
    {
        public DataTablesParam()
        {
            this.Data = new TEntity();
        }

        public TEntity Data { get; set; }

        public Column[] Columns { get; set; }
        public Order[] Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int Draw { get; set; }

        public int PageSize { get { return this.Length > 0 ? this.Length : 10; } }
        public int PageIndex { get { return (this.Start % this.Length == 0 ? this.Start / this.Length : (this.Start / this.Length) + 1) + 1; } }
        public string OrderBy { get { return Columns[Order[0].Column].Data; } }
        public bool OrderByDesc { get { return Order[0].Dir == SortDirection.Desc; } }
    }


    // 排序的方向
    public enum SortDirection
    {
        Asc,    // 升序
        Desc    // 降序
    }

    // 排序列的定义
    public class Order
    {
        public int Column { get; set; }                  // 列序号
        public SortDirection Dir { get; set; }    // 列的排序方向
    }
    public class Search
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }
    // 列定义
    public class Column
    {
        public string Data { get; set; }        // 列名
        public bool Sortable { get; set; }      // 是否可排序
        public bool Searchable { get; set; }    // 是否可搜索
        public Search Search { get; set; }
    }

}