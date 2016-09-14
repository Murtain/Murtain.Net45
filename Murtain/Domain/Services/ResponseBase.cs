using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Services
{


    public abstract class ResponseBase : IResponse
    {
        public ResponseBase()
        {

        }
        public ResponseBase(bool ok)
        {
            this.Ok = ok;
        }
        public ResponseBase(bool ok, string message)
        {
            this.Ok = ok;
            this.Message = message;
        }
        public bool Ok { get; set; }

        public string Message { get; set; }

    }
    public abstract class ResponseBase<T> : IResponse where T : class
    {
        public ResponseBase()
        {

        }
        public ResponseBase(T model)
        {
            this.Model = model;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public T Model { get; set; }
    }
}
