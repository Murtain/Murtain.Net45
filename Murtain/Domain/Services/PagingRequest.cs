using System;

namespace Murtain.Domain.Services
{
    public abstract class PagingRequest : RequestBase
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public bool? SortDesc { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }
    }
}