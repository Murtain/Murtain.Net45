using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.SDK.Models
{
    public class PaginationResponseModel<T> where T : class
    {
        /// <summary>
        /// 总数
        /// </summary>
        public virtual int TotalCount { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        public virtual IEnumerable<T> Models { get; set; }
    }
    public class PaginationRequestModel
    {
        /// <summary>
        /// 当前页
        /// </summary>
        [DefaultValue(1)]
        public virtual int? Page { get; set; }

        /// <summary>
        /// 每页显示数量
        /// </summary>
        [DefaultValue(10)]
        public virtual int? PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual string Sort { get; set; }

        /// <summary>
        /// 是否倒叙
        /// </summary>
        public virtual bool? SortDesc { get; set; }
    }
}
