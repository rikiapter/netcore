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
using Malam.Mastpen.HR.Core.BL.Requests;
using Malam.Mastpen.Core.BL.Services;
using static Malam.Mastpen.API.Commom.Infrastructure.GeneralConsts;
using IdentityModel;

namespace Malam.Mastpen.API.Controllers
{

#pragma warning disable CS1591
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HealthController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly HealthService HealthService;

        public HealthController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 HealthService healthService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            HealthService = healthService;
            HealthService.UserInfo = UserInfo;


        }
#pragma warning restore CS1591


        // GET
        // api/v1/Health/MainScreenHealth/1

        /// <summary>
        ///דשבורד מצב חירום בריאותי
        /// Retrieves a site by Id
        /// </summary>
        /// <param name="Id">Site Id</param>
        /// <returns>A response with site</returns>
        /// <response code="200">Returns the Site </response>
        /// <response code="404">If Employee is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("MainScreenHealth/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> GetMainScreenHealthAsync(int Id)
        {
            var response = await HealthService.GetMainScreenHealthAsync(Id);
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Employee/Employee

        /// <summary>
        /// Retrieves Site Employee
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="SiteId">Site Id</param>
        /// <returns>A response with Site Employee list</returns>
        /// <response code="200">Returns the Site Employee list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Employee")]

        public async Task<IActionResult> GetEmployeesAsync(
            int pageSize = 10,
            int pageNumber = 1,
            int? EmployeeId = null,
            string EmployeeName = null,
            string IdentityNumber = null,
            int? OrganizationId = null,
            int? PassportCountryId = null,
            int? ProffesionType = null,
            int? SiteId = null,
            int? EmployeeIsNotInSiteId = null,
            bool isEmployeeEntry = false,
            bool sortByAuthtorization = false,
            bool sortByTraining = false,
            bool sortByWorkPermit = false)
        {
            var response = await HealthService.GetEmployeesAsync(pageSize, pageNumber, EmployeeId, EmployeeName, IdentityNumber, OrganizationId, PassportCountryId, ProffesionType, SiteId, EmployeeIsNotInSiteId, isEmployeeEntry,
                sortByAuthtorization,
             sortByTraining,
             sortByWorkPermit);

            // Return as http response
            return response.ToHttpResponse();
        }

    }
}