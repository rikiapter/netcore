
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
//    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeWebController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly EmployeeService EmployeeService;

        public EmployeeWebController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 EmployeeService employeeService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            EmployeeService = employeeService;
            EmployeeService.UserInfo = UserInfo;


        }
        // GET
        // api/v1/EmployeeEntry/Employee/1

        /// <summary>
        /// מביא את העובד ע"י guid
        /// מיועד עבור הצהרת בריאות בweb
        /// Retrieves a Employee by guId
        /// </summary>
        /// <param name="Id">Employee Id</param>
        /// <returns>A response with Employee</returns>
        /// <response code="200">Returns the Employee </response>
        /// <response code="404">If Employee is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Employee/{Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> GetEmployeeByGuidEntry(string Guid)
        {
            var responseEmployeeId = await EmployeeService.GetEmployeeByGuidEntry(Guid);
            var responseEquipmentId = await EmployeeService.GetEmployeeEntryByGuidEntry(Guid);

            var responseEmployee = await EmployeeService.GetEmployeeAsync(responseEmployeeId.Model.EmployeeId);
            var site = await EmployeeService.GetSiteByEquipmentIdAsync(responseEquipmentId.Model.EquipmentId ?? 0);

            var TrainingDocs = await EmployeeService.GetTrainingDocsAsync(site.Model.SiteId);//רשימת ההצהרות בטיחות

            var response = new SingleResponse<EmployeeTrainingDocResponse>();
           response.Model = responseEmployeeId.Model.ToEntity( site.Model.SiteId??0, TrainingDocs.Model);
          
            return response.ToHttpResponse();
        }



        // POST
        // api/v1/Employee/EmployeeTraining/

        /// <summary>
        /// Creates a new EmployeeTraining
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new EmployeeTraining</returns>
        /// <response code="200">Returns the EmployeeTraining </response>
        /// <response code="201">A response as creation of EmployeeTraining</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("EmployeeTraining")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostEmployeeTrainingAsync([FromBody]EmployeeTrainingRequest request)
        {
            var entity = request;

            entity.DateFrom = DateTime.Now.Date;
            var response = await EmployeeService.CreateEmployeeTrainingAsync(entity);

            Docs docs = new Docs();
            docs.EntityId = response.Model.EmployeeTrainingId;
            docs.EntityTypeId = (int)EntityTypeEnum.EmployeeTraining;


            var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(EmployeeTraining), request.EmployeeTrainingName, request.fileRequest, (int)DocumentType.Training);
            response.Model.Comment = DOCSResponse.Model.DocumentPath;
            if (DOCSResponse.DIdError)
                throw new Exception("Error in create Document EmployeeTraining" + response.Message);

            return response.ToHttpResponse();
        }
    }
}