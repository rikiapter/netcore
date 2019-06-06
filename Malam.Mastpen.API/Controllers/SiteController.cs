
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.API.Responses;
using Malam.Mastpen.API.Infrastructure;
using Malam.Mastpen.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Malam.Mastpen.API.Security;
using Malam.Mastpen.API.Clients;
using Malam.Mastpen.API.Clients.Contracts;
using Malam.Mastpen.Core.BL.Contracts;

namespace Malam.Mastpen.API.Controllers
{

#pragma warning disable CS1591
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SiteController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly ISiteService SiteService;
     //   protected readonly IGeneralService GeneralService;
        public SiteController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 ISiteService siteService)//,                   IGeneralService generalService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            SiteService = siteService;
       //     GeneralService = generalService;
        }

        // GET
        // api/v1/Site/Site/5

        /// <summary>
        /// Retrieves a Site by Id
        /// משמש ל:
        /// 1.28.1.	שדות לגריד כותרת
        /// 1.29.6.	תיאור שדות נדרשים לאתר בניה
        /// פתיחת מסך עזר לעריכת נתוני אתר
        /// </summary>
        /// <param name="Id">Site Id</param>
        /// <returns>A response with Site</returns>
        /// <response code="200">Returns the Site  list</response>
        /// <response code="404">If Employee is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Site/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSiteAsync(int Id)
        {
            var response = await SiteService.GetSiteAsync(Id);

          //  response.Model = response;

            return response.ToHttpResponse();
        }



        // GET
        // api/v1/Site/SiteByEmployee/5

        /// <summary>
        /// Retrieves a Site by EmployeeId
        /// משמש ל:
        ///פתיחת מסך רשימת אתרים למשתמש
        /// </summary>
        /// <param name="Id">Site Id</param>
        /// <returns>A response with Site</returns>
        /// <response code="200">Returns the Site  list</response>
        /// <response code="404">If Employee is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("SiteByEmployee/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSitesByEmployeeIdAsync(int Id)
        {
            var response = await SiteService.GetSitesByEmployeeIdAsync(Id);
            return response.ToHttpResponse();
        }



  




    }
}