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

using Mailjet.Client;
using Mailjet.Client.Resources;

using Newtonsoft.Json.Linq;
namespace Malam.Mastpen.API.Controllers
{

#pragma warning disable CS1591

    [ApiController]
    [Route("api/v1/[controller]")]
    public class HealthController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly EmployeeService EmployeeService;
        protected readonly HealthService HealthService;
        protected readonly AlertService AlertService;

        public HealthController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 HealthService healthService,
                 AlertService alertService,
                 EmployeeService employeeService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            HealthService = healthService;
            HealthService.UserInfo = UserInfo;
            AlertService = alertService;
            EmployeeService = employeeService;


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
        [Authorize]
        public async Task<IActionResult> GetMainScreenHealthAsync(int Id)
        {
            //נתונים כלליים
            var response = await HealthService.GetMainScreenHealthAsync(Id);

            //התראות
            var responseAlert = await AlertService.GetAlertAsync(Id, 2);
            response.Model.ListAlertsResponse = responseAlert.Model.ToList();

            //אתרים נוספים
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
        [Authorize]
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
            bool sortByWorkPermit = false,
            bool sortHealthDeclaration=false)
        {
            var response = await HealthService.GetEmployeesAsync(pageSize, pageNumber, EmployeeId, EmployeeName, IdentityNumber, OrganizationId, PassportCountryId, ProffesionType, SiteId, EmployeeIsNotInSiteId, isEmployeeEntry,
                sortByAuthtorization,
             sortByTraining,
             sortByWorkPermit,
             sortHealthDeclaration);

            // Return as http response
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Employee/Visit

        /// <summary>
        /// Retrieves Site Visit
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="SiteId">Site Id</param>
        /// <returns>A response with Site Employee list</returns>
        /// <response code="200">Returns the Site Employee list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Visit")]
        [Authorize]
        public async Task<IActionResult> GetVisitAsync(
 
            int SiteId 
          )
        {
            var response = await HealthService.GetVisitHealthDeclarationAsync(     SiteId );

            // Return as http response
            return response.ToHttpResponse();
        }



        // GET
        // api/v1/Health/EmployeesHealthDeclaration

        /// <summary>
        /// הצהרת בריאות
        /// אם שולחים בסוף חיובי מחזיר רק את הרשימה של מי שמילא
        ///ההפך מחזיר רק מי שלא מילא
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="SiteId">Site Id</param>
        /// <returns>A response with Site Employee list</returns>
        /// <response code="200">Returns the Site Employee list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("EmployeesHealthDeclaration")]
        [Authorize]
        public async Task<IActionResult> GetEmployeesHealthDeclarationAsync(
            int pageSize = 10,
            int pageNumber = 1,
            int? OrganizationId = null,
            int? SiteId = null,
            bool isHealthDeclaration = false)
        {
            var response = await HealthService.GetEmployeeHealteDeclarationAsync(pageSize, pageNumber, OrganizationId, SiteId,isHealthDeclaration);
            
            response.Model = response.Model.Where(x => x.DeclarationID > 0 && isHealthDeclaration || x.DeclarationID == 0 && !isHealthDeclaration);
      
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Employee/Organization

        /// <summary>
        /// Retrieves Organization Employee
        /// </summary>

        /// <param name="OrganizationId">Organization Id</param>
        /// <param name="SiteId">Site Id</param>
        /// <returns>A response with Site Employee list</returns>
        /// <response code="200">Returns the Site Employee list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("EmployeeByOrganization")]
 
        public async Task<IActionResult> GetEmployeesByOrganizationAsync(
            int? OrganizationId = null
        
         )
        {
            var response = await HealthService.GetEmployeesByOrganizationAsync(OrganizationId);//, SiteId);

            // Return as http response
            return response.ToHttpResponse();
        }
        // POST
        // api/v1/Employee/Temperature/

        /// <summary>
        /// Creates a new Temperature
        /// מצמיד חום כלשהוא לאדם מסוים
        /// </summary>
        /// <param name="request">Request model</param>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("Temperature")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostEmployeeTemperatureAsync([FromBody]EmployeeTemperature request)
        {
            Random random = new Random();

            request.Temperature = random.Next(36, 39);
            var response = await HealthService.CreateEmployeeTemperatureAsync(request);

            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Employee/EmployeeTraining

        /// <summary>
        /// Retrieves a EmployeeTraining by EmployeeId
        /// בדיקה אם יש הצהרת בריאות תקפה להיום
        ///רשימת הדרכות
        ///1=תדריך בטיחות
        ///4=הצהרת בריאות
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>A response with EmployeeTraining</returns>
        /// <response code="200">Returns the EmployeeTraining list</response>
        /// <response code="404">If EmployeeTraining is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("EmployeeTraining")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEmployeeTrainingAsync(int EmployeeId, int TrainingTypeId)
        {
            var response = await EmployeeService.GetEmployeeTrainingByEmployeeIdAsync(EmployeeId, TrainingTypeId);
            var res = new SingleResponse<EmployeeTrainingRequest>();
            res.Model = response.Model.Where(x => x.DateFrom < DateTime.Now && x.DateTo > DateTime.Now).FirstOrDefault();

            return res.ToHttpResponse();
        }



        // GET
        // api/v1/Health/sendMail

        /// <summary>
        /// Retrieves a sendMail 
        /// </summary>
        /// <param name="sendMail">sendMail</param>

        [HttpGet("sendMail")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public  async Task sendMail()
        {
            {
                MailjetClient client = new MailjetClient(
                    Environment.GetEnvironmentVariable("634a08c3c975a7cd3ef966c18060f758"),
                    Environment.GetEnvironmentVariable("d7c006fbae6f1c952ed8b92f48883f13"))
                {
                    Version = ApiVersion.V3_1,
                };
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
                 .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "rikiapter@gmail.com"},
        {"Name", "riki"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          "rikiapter@gmail.com"
         }, {
          "Name",
          "riki"
         }
        }
       }
      }, {
       "Subject",
       "Greetings from Mailjet."
      }, {
       "TextPart",
       "My first Mailjet email"
      }, {
       "HTMLPart",
       "<h3>Dear passenger 1, welcome to <a href='https://www.mailjet.com/'>Mailjet</a>!</h3><br />May the delivery force be with you!"
      }, {
       "CustomID",
       "AppGettingStartedTest"
      }
     }
                 });
                MailjetResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Console.WriteLine(response.GetData());
                }
                else
                {
                    Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                    Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                    Console.WriteLine(response.GetData());
                    Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
                }
            }
        }
    }
}