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
    public class OrganizationController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly IOrganizationService OrganizationService;
        public OrganizationController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 IOrganizationService organizationService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            OrganizationService = organizationService;
        }


        // POST
        // api/v1/Organization/Organization/

        /// <summary>
        /// Creates a new Organization
        /// 1.34.	מסך מספר 3.2 – קליטת קבלן (ארגון)
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new Organization</returns>
        /// <response code="200">Returns the Site Organization list</response>
        /// <response code="201">A response as creation of Organization</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("Organization")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostOrganizationAsync([FromBody]Organization request)
        {
            var existingEntity = await OrganizationService.GetOrganizationIdAsync(request.OrganizationId);

            if (existingEntity.Model != null)
                ModelState.AddModelError("OrganizationName", "Organization name already exists");

            if (!ModelState.IsValid)
                return BadRequest();

            var entity = request;//.ToEntity();

            entity.UserInsert = UserInfo.UserId;

            var response = await OrganizationService.CreateOrganizationAsync(entity);

            return response.ToHttpResponse();
        }
    }
}