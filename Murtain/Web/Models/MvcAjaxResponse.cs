using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murtain.Web.Models
{
    /// <summary>
    /// This class is used to create standard responses for ajax requests.
    /// </summary>
    [Serializable]
    public class MvcAjaxResponse : MvcAjaxResponse<object>
    {
        /// <summary>
        /// Creates an <see cref="MvcAjaxResponse"/> object.
        /// <see cref="AjaxResponse.Result"/> is set as true.
        /// </summary>
        public MvcAjaxResponse(string successMessage)
        {
            Result = true;
            Message = successMessage;
        }

        /// <summary>
        /// Creates an <see cref="MvcAjaxResponse"/> object with <see cref="AjaxResponse.Result"/> specified.
        /// </summary>
        /// <param name="result">Indicates success status of the result</param>
        public MvcAjaxResponse(bool result, string message)
            : base(result)
        {
            Result = result;
            Message = message;
        }

        /// <summary>
        /// Creates an <see cref="MvcAjaxResponse"/> object with <see cref="AjaxResponse.Data"/> specified.
        /// <see cref="AjaxResponse.Result"/> is set as true.
        /// </summary>
        /// <param name="data">The actual result object of ajax request</param>
        public MvcAjaxResponse(object data)
            : base(data)
        {

        }

        /// <summary>
        /// Creates an <see cref="MvcAjaxResponse"/> object with <see cref="AjaxResponse.Error"/> specified.
        /// <see cref="AjaxResponse.Result"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public MvcAjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }

    public class MvcAjaxResponse<TResult> : AjaxResponse<object> where TResult : class
    {

        /// <summary>
        /// Creates an <see cref="MvcAjaxResponse"/> object.
        /// <see cref="AjaxResponse.Success"/> is set as true.
        /// </summary>
        public MvcAjaxResponse()
        {

        }

        /// <summary>
        /// Creates an <see cref="MvcAjaxResponse"/> object with <see cref="AjaxResponse.Success"/> specified.
        /// </summary>
        /// <param name="result">Indicates success status of the result</param>
        public MvcAjaxResponse(bool result)
            : base(result)
        {

        }

        /// <summary>
        /// Creates an <see cref="MvcAjaxResponse"/> object with <see cref="AjaxResponse.Result"/> specified.
        /// <see cref="AjaxResponse.Success"/> is set as true.
        /// </summary>
        /// <param name="data">The actual result object of ajax request</param>
        public MvcAjaxResponse(TResult data)
            : base(data)
        {

        }

        /// <summary>
        /// Creates an <see cref="MvcAjaxResponse"/> object with <see cref="AjaxResponse.Error"/> specified.
        /// <see cref="AjaxResponse.Success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public MvcAjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {
        }
    }
}