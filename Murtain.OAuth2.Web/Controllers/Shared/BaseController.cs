using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using Murtain.Runtime.Session;
using Murtain.Localization;
using Murtain.GlobalSettings;
using Murtain.Localization.Sources;
using System.Globalization;
using System.Text;
using Murtain.Web.Models;
using Murtain.Exceptions;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Web.Mvc.Async;

namespace Murtain.OAuth2.Web.Controllers.Shared
{
    public class BaseController : Controller
    {

        /// <summary>
        /// MethodInfo for currently executing action.
        /// </summary>
        private MethodInfo _currentMethodInfo;
        public IAppSession AppSession { get; set; }

        public ILocalizationManager LocalizationManager { get; set; }

        public IGlobalSettingManager SettingManager { get; set; }

        public BaseController()
        {
            LocalizationManager = NullLocalizationManager.Instance;
            AppSession = NullAppSession.Instance;
        }

        #region [    Localization    ]

        protected string LocalizationSourceName { get; set; }

        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new Exception("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }

        private ILocalizationSource _localizationSource;

        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }

        #endregion

        #region [    JsonResult    ]

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new NewtonJsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }

        public new JsonResult Json(object data, JsonRequestBehavior jsonRequest)
        {
            return new NewtonJsonResult { Data = data, JsonRequestBehavior = jsonRequest };
        }

        public new JsonResult Json(object data)
        {
            return new NewtonJsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _currentMethodInfo = GetMethodInfo(filterContext.ActionDescriptor);
            base.OnActionExecuting(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //If exception handled before, do nothing.
            //If this is child action, exception should be handled by main action.
            if (filterContext.ExceptionHandled || filterContext.IsChildAction)
            {
                base.OnException(filterContext);
                return;
            }
            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            //if (!filterContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    base.OnException(filterContext);
            //    return;
            //}
            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                base.OnException(filterContext);
                return;
            }
            filterContext.ExceptionHandled = true;

            //Return a special error response to the client.
            filterContext.HttpContext.Response.Clear();
            filterContext.Result = IsJsonResult()
                ? GenerateJsonExceptionResult(filterContext)
                : GenerateNonJsonExceptionResult(filterContext);

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            //Trigger an event, so we can register it.
            //EventBus.Trigger(this, new AbpHandledExceptionData(filterContext.Exception));
        }

        private  MethodInfo GetMethodInfo(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ReflectedActionDescriptor)
            {
                return ((ReflectedActionDescriptor)actionDescriptor).MethodInfo;
            }

            if (actionDescriptor is ReflectedAsyncActionDescriptor)
            {
                return ((ReflectedAsyncActionDescriptor)actionDescriptor).MethodInfo;
            }

            if (actionDescriptor is TaskAsyncActionDescriptor)
            {
                return ((TaskAsyncActionDescriptor)actionDescriptor).MethodInfo;
            }

            throw new Exception("Could not get MethodInfo for the action '" + actionDescriptor.ActionName + "' of controller '" + actionDescriptor.ControllerDescriptor.ControllerName + "'.");
        }
        private ActionResult GenerateNonJsonExceptionResult(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 500;
            return new ViewResult
            {
                ViewName = "Error",
                MasterName = string.Empty,
                ViewData = new ViewDataDictionary<ErrorViewModel>(
                    new ErrorViewModel(filterContext.Exception, BuilderErrorInfo(filterContext))
                    ),
                TempData = filterContext.Controller.TempData
            };

        }

        private ActionResult GenerateJsonExceptionResult(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 500;
            return new NewtonJsonResult(new MvcAjaxResponse(
                    BuilderErrorInfo(filterContext),
                    filterContext.Exception is AuthorizationException
                    ));
        }

        private ErrorInfo BuilderErrorInfo(ExceptionContext filterContext)
        {

            var detailBuilder = new StringBuilder();
            var exception = filterContext.Exception;

            AddExceptionToDetails(exception, detailBuilder);

            var errorInfo = new ErrorInfo(exception.Message, detailBuilder.ToString());

            if (exception is DbEntityValidationException)
            {
                errorInfo.ValidationErrors = GetDbEntityValidationErrorInfos(exception as DbEntityValidationException);
            }
            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is UserFriendlyException || aggException.InnerException is Exceptions.ValidationException)
                {
                    exception = aggException.InnerException;
                }
            }

            if (exception is UserFriendlyException)
            {
                var userFriendlyException = exception as UserFriendlyException;
                errorInfo = new ErrorInfo(userFriendlyException.Code, userFriendlyException.Message, userFriendlyException.Details);
            }

            if (exception is Exceptions.ValidationException)
            {
                errorInfo = new ErrorInfo()
                {
                    ValidationErrors = GetValidationErrorInfos(exception as Exceptions.ValidationException),
                    Details = GetValidationErrorNarrative(exception as Exceptions.ValidationException)
                };
            }

            if (exception is AuthorizationException)
            {
                var authorizationException = exception as AuthorizationException;
                errorInfo = new ErrorInfo(authorizationException.Message);
            }

            return errorInfo;
        }
        private ValidationErrorInfo[] GetDbEntityValidationErrorInfos(DbEntityValidationException dbEntityValidationException)
        {
            var validationErrorInfos = new List<ValidationErrorInfo>();

            foreach (var entityValidationError in dbEntityValidationException.EntityValidationErrors)
            {
                foreach (var validationResult in entityValidationError.ValidationErrors)
                {
                    var validationError = new ValidationErrorInfo(validationResult.ErrorMessage);

                    if (validationResult.PropertyName != null)
                    {
                        validationError.Members = new string[] { validationResult.PropertyName };
                    }
                    validationErrorInfos.Add(validationError);
                }
            }


            return validationErrorInfos.ToArray();
        }

        private void AddExceptionToDetails(Exception exception, StringBuilder detailBuilder)
        {
            //Exception Message
            detailBuilder.AppendLine(exception.GetType().Name + ": " + exception.Message);

            //Additional info for UserFriendlyException
            if (exception is UserFriendlyException)
            {
                var userFriendlyException = exception as UserFriendlyException;
                if (!string.IsNullOrEmpty(userFriendlyException.Details))
                {
                    detailBuilder.AppendLine(userFriendlyException.Details);
                }
            }

            //Additional info for AbpValidationException
            if (exception is Exceptions.ValidationException)
            {
                var validationException = exception as Exceptions.ValidationException;
                if (validationException.ValidationErrors.Count > 0)
                {
                    detailBuilder.AppendLine(GetValidationErrorNarrative(validationException));
                }
            }

            //Exception StackTrace
            if (!string.IsNullOrEmpty(exception.StackTrace))
            {
                detailBuilder.AppendLine("STACK TRACE: " + exception.StackTrace);
            }

            //Inner exception
            if (exception.InnerException != null)
            {
                AddExceptionToDetails(exception.InnerException, detailBuilder);
            }

            //Inner exceptions for AggregateException
            if (exception is AggregateException)
            {
                var aggException = exception as AggregateException;
                if (!aggException.InnerExceptions.Any())
                {
                    return;
                }

                foreach (var innerException in aggException.InnerExceptions)
                {
                    AddExceptionToDetails(innerException, detailBuilder);
                }
            }
        }

        private bool IsJsonResult()
        {
            return typeof(JsonResult).IsAssignableFrom(_currentMethodInfo.ReturnType) ||
                   typeof(Task<JsonResult>).IsAssignableFrom(_currentMethodInfo.ReturnType);
        }

        private static ValidationErrorInfo[] GetValidationErrorInfos(Exceptions.ValidationException validationException)
        {
            var validationErrorInfos = new List<ValidationErrorInfo>();

            foreach (var validationResult in validationException.ValidationErrors)
            {
                var validationError = new ValidationErrorInfo(validationResult.ErrorMessage);

                if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                {
                    validationError.Members = validationResult.MemberNames.ToArray();
                }

                validationErrorInfos.Add(validationError);
            }

            return validationErrorInfos.ToArray();
        }
        private static string GetValidationErrorNarrative(Exceptions.ValidationException validationException)
        {
            var detailBuilder = new StringBuilder();

            foreach (var validationResult in validationException.ValidationErrors)
            {
                detailBuilder.AppendFormat(" - {0}", validationResult.ErrorMessage);
                detailBuilder.AppendLine();
            }

            return detailBuilder.ToString();
        }
    }
}