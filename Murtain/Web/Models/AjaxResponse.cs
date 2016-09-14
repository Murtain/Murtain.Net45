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
    public class AjaxResponse<TResult>
    {
        /// <summary>
        /// Indicates success status of the result.
        /// Set <see cref="Error"/> if this value is false.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// The actual result object of ajax request.
        /// It is set if <see cref="Result"/> is true.
        /// </summary>
        public TResult Data { get; set; }
        /// <summary>
        /// Result message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Error details (Must and only set if <see cref="Result"/> is false).
        /// </summary>
        public ErrorInfo Error { get; set; }

        /// <summary>
        /// This property can be used to indicate that the current user has no privilege to perform this request.
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Data"/> specified.
        /// <see cref="Result"/> is set as true.
        /// </summary>
        /// <param name="data">The actual result object of ajax request</param>
        public AjaxResponse(TResult data)
        {
            Data = data;
            Result = true;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object.
        /// <see cref="Result"/> is set as true.
        /// </summary>
        public AjaxResponse()
        {
            Result = true;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Result"/> specified.
        /// </summary>
        /// <param name="result">Indicates success status of the result</param>
        public AjaxResponse(bool result)
        {
            Result = result;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Error"/> specified.
        /// <see cref="Result"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Result = false;
        }
    }
}