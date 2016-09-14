using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Services
{
    public abstract class PagingResponse<T> : ResponseBase where T : class
    {
        public PagingResponse()
        {

        }
        public PagingResponse(IEnumerable<T> models, int total)
        {
            this.Models = models;
            this.Total = total;
        }
        /// <summary>
        /// 数据模型
        /// </summary>
        public IEnumerable<T> Models { get; set; }

        public int Total { get; set; }
    }
}
