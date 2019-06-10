using System;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using IdentityModel;
using Malam.Mastpen.API.Controllers;
using Malam.Mastpen.API.Commom.Infrastructure;

namespace Malam.Mastpen.API.Filters
{
#pragma warning disable CS1591
    public class MastpenActionFilter : Attribute, IActionFilter
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MastpenActionFilter));
        public MastpenActionFilter(){}

        /// <summary>
        ///  
        /// 
        ///  
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string actionName = context.ActionDescriptor.RouteValues.First().Value;
      
        logger.InfoFormat(GeneralConsts.LOG_INVOKE, actionName);

            var controller = context.Controller as MastpenController;

            foreach (var claim in controller.User.Claims)
            {
                if (claim.Type == JwtClaimTypes.Email)
                    controller.UserInfo.Email = claim.Value;
                else if (claim.Type == JwtClaimTypes.PreferredUserName)
                    controller.UserInfo.UserName = claim.Value;
                else if (claim.Type == JwtClaimTypes.Role)
                    controller.UserInfo.Role = claim.Value;
                else if (claim.Type == JwtClaimTypes.GivenName)
                    controller.UserInfo.GivenName = claim.Value;
                else if (claim.Type == JwtClaimTypes.MiddleName)
                    controller.UserInfo.MiddleName = claim.Value;
                else if (claim.Type == JwtClaimTypes.FamilyName)
                    controller.UserInfo.FamilyName = claim.Value;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string actionName = context.ActionDescriptor.RouteValues.First().Value;

            logger.InfoFormat(GeneralConsts.LOG_SUCCESS, actionName);

        }
    }
#pragma warning restore CS1591
}
