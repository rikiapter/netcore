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

namespace Malam.Mastpen.API.Controllers
{

#pragma warning disable CS1591
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrganizationController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly OrganizationService OrganizationService;
        public OrganizationController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 OrganizationService organizationService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            OrganizationService = organizationService;
            OrganizationService.UserInfo = UserInfo;
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
            var existingEntity = await OrganizationService.GetOrganizationByNameAsync(request.OrganizationName);

            if (existingEntity.Model != null)
                ModelState.AddModelError("OrganizationName", "Organization name already exists");

            if (!ModelState.IsValid)
                throw new Exception();

           

            var entity = request;//.ToEntity();
            if (request.OrganizationParentId != null)
                entity.OrganizationTypeId = (int)OrganizationTypeEnum.SecondaryOrganization;

            var response = await OrganizationService.CreateOrganizationAsync(entity);

            entity.OrganizationFaceGroup = "facegroup" + response.Model.OrganizationId; 
            var response2 = await OrganizationService.UpdateOrganizationAsync(entity);

            return response.ToHttpResponse();
        }


        // GET
        // api/v1/Organization/Organization

        /// <summary>
        /// Retrieves Site Organization
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="OrganizationId">Organization Id</param>
        /// <param name="SiteId">Site Id</param>
        /// <param name="DateFrom">Date From</param>
        /// <param name="DateTo">Date To</param>
        /// <returns>A response with Site Organization list</returns>
        /// <response code="200">Returns the Site Organization list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Organization")]
        //  [Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> GetOrganizationsAsync(int pageSize = 10, int pageNumber = 1, int? OrganizationId = null, string OrganizationName = null,int? OrganizationNumber=null,int? OrganizationExpertiseTypeId= null,int ? OrganizationParentId = null)
        {
            var response = await OrganizationService.GetOrganizationsAsync(pageSize, pageNumber, OrganizationId, OrganizationName,  OrganizationNumber,  OrganizationExpertiseTypeId , OrganizationParentId);

            // Return as http response
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Organization/Organization/5

        /// <summary>
        /// Retrieves a Organization by Id
        /// 1.33.	מסך מספר 2.3 – תיק עובד
        /// 1.33.13.	לשונית לפרטים אישיים ורישיון עבודה
        /// </summary>
        /// <param name="Id">Organization Id</param>
        /// <returns>A response with Organization</returns>
        /// <response code="200">Returns the Site Organization list</response>
        /// <response code="404">If Organization is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Organization/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        //[Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> GetOrganizationAsync(int Id)
        {
            var response = await OrganizationService.GetOrganizationAsync(Id);
            return response.ToHttpResponse();
        }


        // PUT
        // api/v1/Organization/Organization/5

        /// <summary>
        /// Updates an existing Organization
        /// </summary>
        /// <param name="Id">Organization Id</param>
        /// <param name="request">Request model</param>
        /// <returns>A response as update Organization result</returns>
        /// <response code="200">If Organization was updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("Organization/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutOrganizationAsync(int Id, [FromBody]OrganizationRequest request)
        {   // Get Organization by Id
            var entity = await OrganizationService.GetOrganizationsAsync(request.OrganizationId??0);
            //// ValIdate if entity exists
            if (entity == null)
                return NotFound();

            var Organization = request;//.ToEntity();


            Organization.OrganizationId = Id;
            var response = await OrganizationService.UpdateOrganizationAsync(Organization);

            response.Message = string.Format("Sucsses Put for Site Organization = {0} ", request.OrganizationId);


            return response.ToHttpResponse();
        }

        // DELETE
        // api/v1/Organization/Organization/5

        /// <summary>
        /// Deletes an existing Organization
        /// </summary>
        /// <param name="Id">Organization Id</param>
        /// <returns>A response as delete Organization result</returns>
        /// <response code="200">If Organization was deleted successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("Organization/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteOrganizationAsync(int Id)
        {

            //// Get Organization by Id
            var entity = await OrganizationService.GetOrganizationsAsync(Id);

            //// ValIdate if entity exists
            if (entity == null)
                return NotFound();

            //// Remove entity from repository
            var response = await OrganizationService.DeleteOrganizationAsync(Id);
            //DbContext.Remove(entity);

            //response.Message = string.Format("Sucsses Delete Site Organization = {0} ", Id);

            //// Delete entity in database
            //await DbContext.SaveChangesAsync();

            return response.ToHttpResponse();
        }
    }
}