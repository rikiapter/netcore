
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
using Malam.Mastpen.Core.DAL.Dbo;

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
        protected readonly AlertService AlertService;
        protected readonly HealthService HealthService;

        public EmployeeWebController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 EmployeeService employeeService,
                 AlertService alertService,
                 HealthService healthService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            EmployeeService = employeeService;
            AlertService = alertService;
            HealthService = healthService;
            EmployeeService.UserInfo = UserInfo;


        }
        //to delete
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

                //var responseEmployee = await EmployeeService.GetEmployeeAsync(responseEmployeeId.Model.EmployeeId);
                var site = await EmployeeService.GetSiteByEquipmentIdAsync(responseEquipmentId.Model.EquipmentId ?? 0);
                var ListGenTextSystem = await EmployeeService.GetGenTextSystemAsync(site.Model.Site.OrganizationId);
                //var TrainingDocs = await EmployeeService.GetTrainingDocsAsync(null,site.Model.SiteId,null,4);//רשימת ההצהרות בטיחות
                //var ListTrainingDocs = new List<ParameterCodeEntity>();
                //var ListLanguage = new List<ParameterCodeEntity>();
                //foreach (var t in TrainingDocs.Model)
                //{
                //    ListTrainingDocs.Add(new ParameterCodeEntity
                //    {
                //        ParameterFieldID = t.LanguageId, //שפה
                //        Name = t.DocumentPath            //נתיב
                //    });
                //}
                //foreach ( var t in TrainingDocs.Model)
                //{
                //    ListLanguage.Add(new ParameterCodeEntity
                //    {
                //        ParameterFieldID = t.LanguageId,
                //        Name = t.Language.LanguageName
                //    });
                //}

                var response = new SingleResponse<EmployeeTrainingDocResponse>();
                response.Model = responseEmployeeId.Model.ToEntity(site.Model.SiteId ?? 0, ListGenTextSystem.Model);

                return response.ToHttpResponse();
            
         
        }

        //to delete
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
        [HttpGet("TextHealthDeclaration/{Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> GetTextHealthDeclarationByGuidEntry(string Guid, string Type)
        {
            if (Type == "hr")
            {
                var responseEmployeeId = await EmployeeService.GetEmployeeByGuidEntry(Guid);
                var responseEquipmentId = await EmployeeService.GetEmployeeEntryByGuidEntry(Guid);

                //var responseEmployee = await EmployeeService.GetEmployeeAsync(responseEmployeeId.Model.EmployeeId);
                var site = await EmployeeService.GetSiteByEquipmentIdAsync(responseEquipmentId.Model.EquipmentId ?? 0);
                var ListGenTextSystem = await EmployeeService.GetGenTextSystemAsync(site.Model.Site.OrganizationId);
                //var TrainingDocs = await EmployeeService.GetTrainingDocsAsync(null,site.Model.SiteId,null,4);//רשימת ההצהרות בטיחות
                //var ListTrainingDocs = new List<ParameterCodeEntity>();
                //var ListLanguage = new List<ParameterCodeEntity>();
                //foreach (var t in TrainingDocs.Model)
                //{
                //    ListTrainingDocs.Add(new ParameterCodeEntity
                //    {
                //        ParameterFieldID = t.LanguageId, //שפה
                //        Name = t.DocumentPath            //נתיב
                //    });
                //}
                //foreach ( var t in TrainingDocs.Model)
                //{
                //    ListLanguage.Add(new ParameterCodeEntity
                //    {
                //        ParameterFieldID = t.LanguageId,
                //        Name = t.Language.LanguageName
                //    });
                //}

                var response = new SingleResponse<EmployeeTrainingDocResponse>();
                response.Model = responseEmployeeId.Model.ToEntity(site.Model.SiteId ?? 0, ListGenTextSystem.Model);

                return response.ToHttpResponse();
            }
            if (Type == "site")
            {
                var responseSite = await EmployeeService.GetSiteByGuid(Guid);
                var ListGenTextSystem = await EmployeeService.GetGenTextSystemAsync(responseSite.Model.OrganizationId);
        
                var response = new SingleResponse<EmployeeTrainingDocResponse>();
                response.Model = responseSite.Model.ToEntity(ListGenTextSystem.Model);

                return response.ToHttpResponse();
            }
            else
                    return NotFound();

        }

        // POST
        // api/v1/Employee/HealthDeclaration/

        /// <summary>
        /// Creates a new HealthDeclaration
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new HealthDeclaration</returns>
        /// <response code="200">Returns the HealthDeclaration </response>
        /// <response code="201">A response as creation of HealthDeclaration</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("HealthDeclaration")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostHealthDeclarationAsync([FromBody]HealthDeclaration request)
        {
           
            //EntityTypeId=2 עובדים
            //EntityTypeId=11 מבקרים
            //מבקר בינתיים יכול להיות ריק 
          //  אם הוא לא מקושר לטבלת מבקרים

            var entity = request;

            entity.Date = DateTime.Now.Date;
   
            var response = await HealthService.CreateHealthDeclarationAsync(entity);

         
            var Alert = new Alerts();
            Alert.EntityId = request.EntityId;
            Alert.EntityTypeId = request.EntityTypeId ;// עובדים
            Alert.AlertTypeId = 7;//הצהרת בריאות
            Alert.SiteId = request.SiteId;

            var responseAlert = await AlertService.GetAlertAsync(Alert);

            if (responseAlert.Model != null)
            {
                responseAlert.Model.AlertStatusId = 2;//נקרא
                await AlertService.UpdateAlertAsync(responseAlert.Model);
            }
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
            entity.DateTo = DateTime.Now.AddDays(1).Date;
            var response = await EmployeeService.CreateEmployeeTrainingAsync(entity);

            Docs docs = new Docs();
            docs.EntityId = response.Model.EmployeeTrainingId;
            docs.EntityTypeId = (int)EntityTypeEnum.EmployeeTraining;


            var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(EmployeeTraining), request.EmployeeTrainingName, request.fileRequest, (int)DocumentType.Training);
            response.Model.Comment = DOCSResponse.Model.DocumentPath;
         
            if (DOCSResponse.DIdError)
                throw new Exception("Error in create Document EmployeeTraining" + response.Message);


          
            var Alert = new Alerts();


            Alert.EntityId= request.EmployeeId;
            Alert.EntityTypeId = 2;// עובדים
            Alert.AlertTypeId = 7;//הצהרת בריאות
            Alert.SiteId = request.SiteId;

            var responseAlert = await AlertService.GetAlertAsync(Alert);
            responseAlert.Model.AlertStatusId=2;//נקרא
            if (responseAlert.Model !=null)
                    await AlertService.UpdateAlertAsync(responseAlert.Model);
            
            return response.ToHttpResponse();
        }
    }
}