using System.Security.Claims;
using DotNetIdentity.Models;
using DotNetIdentity.Models.ViewModels;
using DotNetIdentity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;
using Microsoft.AspNetCore.Diagnostics;

namespace DotNetIdentity.Controllers {
    /// \todo add translation
    /// <summary>
    /// Controller for error views
    /// </summary>
    public class ErrorsController : Controller
    {
        /// <summary>
        /// controller action for ErrorEx view
        /// </summary>
        /// <returns>view ErrorEx</returns>
        public IActionResult ErrorEx()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                if(exceptionFeature.Error.StackTrace!=null) {
                    ViewBag.StackTrace = exceptionFeature.Error.StackTrace;
                }
                ViewBag.ErrorMessage = exceptionFeature.Error.Message;
                ViewBag.RouteOfException = exceptionFeature.Path;
            }

            return View();
        }

        /// <summary>
        /// Controller action for ErrorCd view
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns> view ErrorCd</returns>
        [Route("Errors/ErrorCd/{statusCode}")]
        public IActionResult ErrorCd(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            StatusCodesModel md = new StatusCodesModel();
            md.StatusCode = statusCode;
            md.RouteOfException = statusCodeData!.OriginalPath;

            switch (statusCode)
            {
                // request timeout
                case 408:
                    md.ErrorMessage = "Timeout. The request timed out..";
                    break;
                // badrequest
                case 400:
                    md.ErrorMessage = "Badrequest. The request meets not the controllers expetation.";
                    break;
                // forbidden
                case 403:
                    md.ErrorMessage = "Acess denied!";
                    break;
                // unauthorized
                case 401:
                    md.ErrorMessage = "You're not authorized to access this resource!";
                    break;
                //page not found
                case 404:
                    md.ErrorMessage = "Sorry the page you requested could not be found";                    
                    break;
                // server error
                case 500:
                    md.ErrorMessage = "Sorry something went wrong on the server";
                    break;
                // service unavailable
                case 503:
                    md.ErrorMessage = "Sorry, the service is currently unavailable.";
                    break;
                default:
                    md.ErrorMessage = "Sorry, there was an error.";
                    break;
            }

            return View("ErrorCd", md);
        }
    }
}