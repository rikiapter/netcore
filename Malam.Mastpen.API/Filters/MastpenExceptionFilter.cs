﻿using Malam.Mastpen.Core.BL;
using Malam.Mastpen.Core.BL.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Malam.Mastpen.API.Filters
{
    /// <summary>
    /// תופס את כל השגיאות במערכת
    ///יש להוסיף את הלוגר
    ///log4net
    /// </summary>
    public class MastpenExceptionFilter : IExceptionFilter
    {
        private static readonly log4net.ILog logger  = log4net.LogManager.GetLogger(typeof(MastpenActionFilter));
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.RouteValues.First().Value;
            logger.Info("test log");
            HttpStatusCode status = HttpStatusCode.InternalServerError;
           

            var exceptionType = context.Exception.GetType();
          
            context.ExceptionHandled = true;
            var response = new PagedResponse<Core.DAL.Entities.Employee>();


            response.DIdError = true;

            if (exceptionType is MastpenException)
            {
                logger.ErrorFormat( "There was an error on '{0}': {1}", actionName, context.Exception.Message);

                response.ErrorMessage = string.Format("There was an error on '{0}': {1} ,Massage {2}", actionName, context.Exception.Message, context.Exception.Message);
            }
            else
            {
                logger.ErrorFormat("There was a critical error on '{0}': {1}", actionName, context.Exception.Message);
          
                response.ErrorMessage = string.Format("There was an internal error on '{0}': {1}, please contact to technical support.", actionName, context.Exception.Message);
            }
            context.Result = new ObjectResult(response);
        }
    }
}
