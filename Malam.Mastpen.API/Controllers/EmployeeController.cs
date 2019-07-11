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
using Malam.Mastpen.HR.API.Infrastructure;
using Malam.Mastpen.HR.Core.BL.Requests;
using Malam.Mastpen.Core.BL.Services;
using static Malam.Mastpen.API.Commom.Infrastructure.GeneralConsts;

namespace Malam.Mastpen.API.Controllers
{

#pragma warning disable CS1591
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly EmployeeService EmployeeService;
        protected readonly BlobStorageService blobStorageService;
        public EmployeeController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 EmployeeService employeeService,BlobStorageService blobStorageService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            EmployeeService = employeeService;
            this.blobStorageService = blobStorageService;
        }
#pragma warning restore CS1591



        // GET
        // api/v1/Employee/Employee

        /// <summary>
        /// Retrieves Site Employee
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="EmployeeId">Employee Id</param>
        /// <param name="SiteId">Site Id</param>
        /// <param name="DateFrom">Date From</param>
        /// <param name="DateTo">Date To</param>
        /// <returns>A response with Site Employee list</returns>
        /// <response code="200">Returns the Site Employee list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Employee")]
       // [Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> GetEmployeesAsync(int pageSize = 10, int pageNumber = 1, int? EmployeeId = null, string EmployeeName = null, string IdentityNumber = null,int? OrganizationId=null,int? PassportCountryId=null,int? ProffesionType=null, int? SiteId=null)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var response = await EmployeeService.GetEmployeesAsync(pageSize, pageNumber, EmployeeId, EmployeeName ,  IdentityNumber, OrganizationId,PassportCountryId, ProffesionType, SiteId);

            // Return as http response
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Employee/Employee/5

        /// <summary>
        /// Retrieves a Employee by Id
        /// 1.33.	מסך מספר 2.3 – תיק עובד
        /// 1.33.13.	לשונית לפרטים אישיים ורישיון עבודה
        /// </summary>
        /// <param name="Id">Employee Id</param>
        /// <returns>A response with Employee</returns>
        /// <response code="200">Returns the Site Employee list</response>
        /// <response code="404">If Employee is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Employee/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        //יש להעלות את הפרויקט 
        //identity server
        //[Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> GetEmployeeAsync(int Id)
        {
            var response = await EmployeeService.GetEmployeeAsync(Id);
            return response.ToHttpResponse();
        }

        // POST
        // api/v1/Employee/Employee/

        /// <summary>
        /// Creates a new Employee
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new Employee</returns>
        /// <response code="200">Returns the Site Employee list</response>
        /// <response code="201">A response as creation of Employee</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("Employee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostEmployeeAsync([FromBody]EmployeeRequest request)
        {
            var existingEntity = await EmployeeService.GetEmployeeByEmployeeNameAsync(request.EmployeeId ?? 0);

            if (existingEntity.Model != null)
                ModelState.AddModelError("EmployeeName", "Employee name already exists");

            if (!ModelState.IsValid)
                return BadRequest();

            var entity = request.ToEntity();

            entity.UserInsert = UserInfo.UserId;

            var employeeResponse = await EmployeeService.CreateEmployeeAsync(entity);

            PhoneMail phoneMail = new PhoneMail();
            phoneMail.PhoneNumber = request.PhoneNumber;
            phoneMail.EntityTypeId = 1;
            phoneMail.EntityId = employeeResponse.Model.EmployeeId;

            var phoneNumberResponse = await EmployeeService.CreatePhoneMailAsync(phoneMail, typeof(Employee));
            string fileUrl;
            
            //upload picture to blob
            if (request.picture != null)
            {
                 fileUrl = blobStorageService.UploadFileToBlob(request.IdentityNumber + request.FirstNameEN + "_" + request.LastNameEN, request.picture);

               //get personid from face recognition 
               //  EmplyeePicture emplyeePicture = new EmplyeePicture();
               //  emplyeePicture.EmployeeId = employeeResponse.Model.EmployeeId;

               //save file path in db
               // var emplyeePictureResponse = await EmployeeService.CreateEmplyeePictureAsync(emplyeePicture);

                Docs docs = new Docs();
                docs.DocumentPath = fileUrl;
                docs.EntityId= employeeResponse.Model.EmployeeId;
                docs.EntityTypeId=(int)EntityTypeEnum.Employee;
                docs.DocumentTypeId = (int)DocumentType.FaceImage;
                var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee)); 
            }

            //upload IdentityFile to blob
            if (request.IdentityFile != null)
            {
                fileUrl = blobStorageService.UploadFileToBlob(request.IdentityNumber + request.FirstNameEN + "_" + request.LastNameEN, request.IdentityFile);
                Docs docs = new Docs();
                docs.DocumentPath = fileUrl;
                docs.EntityId = employeeResponse.Model.EmployeeId;
                docs.EntityTypeId = (int)EntityTypeEnum.Employee;
                docs.DocumentTypeId= (int)DocumentType.CopyofID;
                var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee));
            }

            if (request.PassportFile != null)
            {
                fileUrl = blobStorageService.UploadFileToBlob(request.IdentityNumber + request.FirstNameEN + "_" + request.LastNameEN, request.IdentityFile);
                Docs docs = new Docs();
                docs.DocumentPath = fileUrl;
                docs.EntityId = employeeResponse.Model.EmployeeId;
                docs.EntityTypeId = (int)EntityTypeEnum.Employee;
                docs.DocumentTypeId = (int)DocumentType.CopyPassport;
                var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee));
            }

            return employeeResponse.ToHttpResponse();
        }

        // PUT
        // api/v1/Employee/Employee/5

        /// <summary>
        /// Updates an existing Employee
        /// </summary>
        /// <param name="Id">Employee Id</param>
        /// <param name="request">Request model</param>
        /// <returns>A response as update Employee result</returns>
        /// <response code="200">If Employee was updated successfully</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("Employee/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutEmployeeAsync(int Id, [FromBody]EmployeeRequest request)
        {   // Get Employee by Id
            var entity = await EmployeeService.GetEmployeesAsync(request.EmployeeId ?? 0);
            //// ValIdate if entity exists
            if (entity == null)
                return NotFound();

            var employee = request.ToEntity();

            employee.UserInsert = UserInfo.UserId;
            employee.EmployeeId = Id;
            var response = await EmployeeService.UpdateEmployeeAsync(employee);

            response.Message = string.Format("Sucsses Put for Site Employee = {0} ", request.EmployeeId);


            return response.ToHttpResponse();
        }

        // DELETE
        // api/v1/Employee/Employee/5

        /// <summary>
        /// Deletes an existing Employee
        /// </summary>
        /// <param name="Id">Employee Id</param>
        /// <returns>A response as delete Employee result</returns>
        /// <response code="200">If Employee was deleted successfully</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("Employee/{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteEmployeeAsync(int Id)
        {

            //// Get Employee by Id
            var entity = await EmployeeService.GetEmployeesAsync(Id);

            //// ValIdate if entity exists
            if (entity == null)
                return NotFound();

            //// Remove entity from repository
            var response = await EmployeeService.DeleteEmployeeAsync(Id);
            //DbContext.Remove(entity);

            //response.Message = string.Format("Sucsses Delete Site Employee = {0} ", Id);

            //// Delete entity in database
            //await DbContext.SaveChangesAsync();

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
            var entity =  request;

            entity.UserInsert = UserInfo.UserId;

            var response = await EmployeeService.CreateEmployeeTrainingAsync(entity);

            if (request.fileRequest != null)
            {
              var  fileUrl = blobStorageService.UploadFileToBlob(request.EmployeeTrainingName , request.fileRequest);

                //save in docs
                Docs docs = new Docs();
                docs.DocumentPath = fileUrl;
                docs.EntityId = response.Model.EmployeeTrainingId;
                docs.EntityTypeId = (int)EntityTypeEnum.EmployeeTraining;
                docs.DocumentTypeId =(int)DocumentType.Training;
                var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(EmployeeTraining));
            }
            return response.ToHttpResponse();
        }

        // POST
        // api/v1/Employee/EmployeeWorkPermit/

        /// <summary>
        /// Creates a new EmployeeWorkPermit
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new EmployeeWorkPermit</returns>
        /// <response code="200">Returns the EmployeeWorkPermit </response>
        /// <response code="201">A response as creation of EmployeeWorkPermit</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("EmployeeWorkPermit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostEmployeeWorkPermitAsync([FromBody]EmployeeWorkPermitRequest request)
        {

            var entity = request;

            entity.UserInsert = UserInfo.UserId;

            var response = await EmployeeService.CreateEmployeeWorkPermitAsync(entity);

            if (request.fileRequest != null)
            {
                var fileUrl = blobStorageService.UploadFileToBlob(request.EmployeeWorkPermitName, request.fileRequest);

                //save in docs
                Docs docs = new Docs();
                docs.DocumentPath = fileUrl;
                docs.EntityId = response.Model.EmployeeWorkPermitId;
                docs.EntityTypeId = (int)EntityTypeEnum.EmployeeWorkPermit;
                docs.DocumentTypeId = (int)DocumentType.CopyWorkPermit;
                var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(EmployeeWorkPermit));
            }
            return response.ToHttpResponse();
        }

        // POST
        // api/v1/Employee/EmployeeAuthtorization/

        /// <summary>
        /// Creates a new EmployeeAuthtorization
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new EmployeeAuthtorization</returns>
        /// <response code="200">Returns the EmployeeAuthtorization </response>
        /// <response code="201">A response as creation of EmployeeAuthtorization</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("EmployeeAuthtorization")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostEmployeeAuthtorizationAsync([FromBody]EmployeeAuthtorizationRequest request)
        {

            var entity = request;

            entity.UserInsert = UserInfo.UserId;

            var response = await EmployeeService.CreateEmployeeAuthtorizationAsync(entity);

            if (request.fileRequest != null)
            {
                var fileUrl = blobStorageService.UploadFileToBlob(request.EmployeeAuthorizationName, request.fileRequest);

                //save in docs
                Docs docs = new Docs();
                docs.DocumentPath = fileUrl;
                docs.EntityId = response.Model.EmployeeAuthorizationId;
                docs.EntityTypeId = (int)EntityTypeEnum.EmployeeAuthtorization;
                docs.DocumentTypeId = (int)DocumentType.Authtorization;
                var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(EmployeeAuthtorization));
            }
            return response.ToHttpResponse();
        }


        // POST
        // api/v1/Employee/Notes/

        /// <summary>
        /// Creates a new Notes
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new EmployeeAuthtorization</returns>
        /// <response code="200">Returns the EmployeeAuthtorization </response>
        /// <response code="201">A response as creation of EmployeeAuthtorization</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("Notes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostEmployeeNoteAsync([FromBody]Notes request)
        {

            var entity = request;

            entity.UserInsert = UserInfo.UserId;

            var response = await EmployeeService.CreateEmployeeNoteAsync(entity);

            return response.ToHttpResponse();
        }


        // GET
        // api/v1/Employee/EmployeeTraining

        /// <summary>
        /// Retrieves a EmployeeTraining by EmployeeId
        ///רשימת הדרכות
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
        public async Task<IActionResult> GetEmployeeTrainingAsync(int EmployeeId)
        {
            var response = await EmployeeService.GetEmployeeTrainingByEmployeeIdAsync(EmployeeId);
            return response.ToHttpResponse();
        }


        // GET
        // api/v1/Employee/EmployeeWorkPermit

        /// <summary>
        /// Retrieves a EmployeeWorkPermit by EmployeeId
        ///רשימת הדרכות
        /// </summary>
        /// <param name="EmployeeId">EmployeeWorkPermit Id</param>
        /// <returns>A response with EmployeeWorkPermit</returns>
        /// <response code="200">Returns the EmployeeWorkPermit list</response>
        /// <response code="404">If EmployeeWorkPermit is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("EmployeeWorkPermit")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEmployeeWorkPermitAsync(int EmployeeId)
        {
            var response = await EmployeeService.GetEmployeeWorkPermitByEmployeeIdAsync(EmployeeId);
            return response.ToHttpResponse();
        }


        // GET
        // api/v1/Employee/EmployeeAuthtorization

        /// <summary>
        /// Retrieves a EmployeeAuthtorization by EmployeeId
        ///רשימת הדרכות
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>A response with EmployeeAuthtorization</returns>
        /// <response code="200">Returns the EmployeeAuthtorization list</response>
        /// <response code="404">If EmployeeAuthtorization is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("EmployeeAuthtorization")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEmployeeAuthtorizationAsync(int EmployeeId)
        {
            var response = await EmployeeService.GetEmployeeAuthtorizationByEmployeeIdAsync(EmployeeId);
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Employee/Notes

        /// <summary>
        /// Retrieves a Notes by EmployeeId
        ///רשימת הדרכות
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>A response with Notes</returns>
        /// <response code="200">Returns the Notes list</response>
        /// <response code="404">If EmployeeAuthtorization is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("Notes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEmployeeNoteAsync(int EmployeeId)
        {
            var response = await EmployeeService.GetEmployeeNoteByEmployeeIdAsync(EmployeeId);
            return response.ToHttpResponse();
        }
    }
}