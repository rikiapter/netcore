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
    public class EmployeeController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly EmployeeService EmployeeService;
    
        public EmployeeController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 EmployeeService employeeService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            EmployeeService = employeeService;
            EmployeeService.UserInfo = UserInfo;


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
            int? OrganizationId=null,
            int? PassportCountryId=null,
            int? ProffesionType=null,
            int? SiteId=null,
            int? EmployeeIsNotInSiteId=null,
            bool isEmployeeEntry=false,
            bool sortByAuthtorization=false,
            bool sortByTraining=false,
            bool sortByWorkPermit=false)
        { 
            var response = await EmployeeService.GetEmployeesAsync(pageSize, pageNumber, EmployeeId, EmployeeName ,  IdentityNumber, OrganizationId,PassportCountryId, ProffesionType, SiteId, EmployeeIsNotInSiteId, isEmployeeEntry ,
                sortByAuthtorization ,
             sortByTraining ,
             sortByWorkPermit );

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
     //   [Authorize]//(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> GetEmployeeAsync(int Id)
        {
            var response = await EmployeeService.GetEmployeeAsync(Id);
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Employee/EmployeeByUserName/riki

        /// <summary>
        /// Retrieves a Employee by UserName
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <returns>A response with Employee</returns>
        /// <response code="200">Returns the  Employee </response>
        /// <response code="404">If Employee is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("EmployeeByUserUserName/{UserName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        //יש להעלות את הפרויקט 
        //identity server
        //   [Authorize]//(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> GetEmployeeByUserNameAsync(string UserName)
        {

            var response = await EmployeeService.GetEmployeeByUserIdAsync(UserName);
            return response.ToHttpResponse();
        }


        // Post
        // api/v1/Employee/EmployeeByUserName/riki

        /// <summary>
        /// Retrieves a Employee by UserName
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <returns>A response with Employee</returns>
        /// <response code="200">Returns the  Employee </response>
        /// <response code="404">If Employee is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("User")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        //יש להעלות את הפרויקט 
        //identity server
        //   [Authorize]//(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> PostUserAsync([FromBody] UsersRequest request)
        {
            // Get Employee by Id
            var employee = await EmployeeService.GetEmployeesAsync(request.EmployeeId ?? 0);
            //// ValIdate if entity exists
            if (employee == null)
                return NotFound();

            //אם קיים שם משתמש
            var user = await EmployeeService.GetEmployeeByUserIdAsync(request.UserName);
            if (user != null)
               throw new Exception("user name is alredy exist");

            //אם קיים משתמש עם employee זה לא תקין
            var useremployee = await EmployeeService.GetUserByEmployeeIdAsync(request.EmployeeId??0);
            if (useremployee != null)
                throw new Exception("user employee is alredy exist");

            var entity = request.ToEntity();
            var response = await EmployeeService.CreateUserAsync(entity);
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
            if (!request.AgreeOnTheBylaws)
                ModelState.AddModelError("AgreeOnTheBylaws", "Employee not Agree On The Bylaws");

            var existingEntity = await EmployeeService.GetEmployeeByIdentityNumberAsync(request.IdentityNumber);

                if (existingEntity.Model != null)
                    ModelState.AddModelError("EmployeeIdentityNumber", "Employee IdentityNumber already exists");

                if (!ModelState.IsValid)
                    throw new Exception();

                var entity = request.ToEntity();

                entity.UserInsert = UserInfo.UserId;

                var employeeResponse = await EmployeeService.CreateEmployeeAsync(entity);
                if (request.PhoneNumber != null)
                {
                    PhoneMail phoneMail = new PhoneMail();
                    phoneMail.PhoneNumber = request.PhoneNumber;
                    phoneMail.Email = request.Email;
                    phoneMail.EntityTypeId = (int)EntityTypeEnum.Employee;
                    phoneMail.EntityId = employeeResponse.Model.EmployeeId;

                    var res = await EmployeeService.CreatePhoneMailAsync(phoneMail, typeof(Employee));
                    if (res.DIdError)
                        employeeResponse.SetMessageErrorCreate(nameof(PhoneMail), res.Message);
                }

                if (request.ProffesionTypeId != null)
                {
                    EmployeeProffesionType proffesionType = new EmployeeProffesionType();
                    proffesionType.ProffesionTypeId = request.ProffesionTypeId;
                    proffesionType.EmployeeId = employeeResponse.Model.EmployeeId;

                    var res = await EmployeeService.CreateEmployeeProffesionTypeAsync(proffesionType, typeof(Employee));
                    if (res.DIdError)
                        employeeResponse.SetMessageErrorCreate(nameof(ProffesionType), res.Message);
                }

                Docs docs = new Docs();
                docs.EntityTypeId = (int)EntityTypeEnum.Employee;
                docs.EntityId = employeeResponse.Model.EmployeeId;
                var fileName = employeeResponse.Model.IdentityNumber;

                //יש לבדוק אם אכן זה עובד
                //upload picture to blob
                var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee), fileName, request.picture, (int)DocumentType.FaceImage);
                employeeResponse.Model.picturePath = DOCSResponse.Model.DocumentPath;
                if (DOCSResponse.DIdError)
                    employeeResponse.SetMessageErrorUpdate("FaceImage", DOCSResponse.Message);

                DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee), fileName, request.IdentityFile, (int)DocumentType.CopyofID);
                employeeResponse.Model.IdentityFilePath = DOCSResponse.Model.DocumentPath;
                if (DOCSResponse.DIdError)
                    employeeResponse.SetMessageErrorUpdate("CopyofID", DOCSResponse.Message);

                DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee), fileName, request.PassportFile, (int)DocumentType.CopyPassport);
                employeeResponse.Model.PassportFilePath = DOCSResponse.Model.DocumentPath;
                if (DOCSResponse.DIdError)
                    employeeResponse.SetMessageErrorUpdate("CopyPassport", DOCSResponse.Message);

                SiteEmployee siteEmployee = new SiteEmployee();
                siteEmployee.EmployeeId = employeeResponse.Model.EmployeeId;
                siteEmployee.SiteId = request.SiteId;
                var response = await EmployeeService.CreateSiteEmployeeAsync(siteEmployee);

                return employeeResponse.ToHttpResponse();
   

        }


        // POST
        // api/v1/Employee/EmplyeePicture/

        /// <summary>
        /// Creates a new EmplyeePicture
        /// </summary>
        /// <param name="request">Request model</param>
        /// <returns>A response with new EmplyeePicture</returns>
        /// <response code="200">Returns the EmplyeePicture list</response>
        /// <response code="201">A response as creation of EmplyeePicture</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("EmplyeePicture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostEmplyeePictureAsync([FromBody]EmplyeePicture request)
        {
            var Response = await EmployeeService.CreateEmplyeePictureAsync(request);
            return Response.ToHttpResponse();
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
        {
            // Get Employee by Id
            var entity = await EmployeeService.GetEmployeesAsync(request.EmployeeId ?? 0);
            //// ValIdate if entity exists
            if (entity == null)
                return NotFound();

            var employee = request.ToEntity();

          //  employee.UserInsert = UserInfo.UserId;
            employee.EmployeeId = Id;

            var employeeResponse = await EmployeeService.UpdateEmployeeAsync(employee);

            employeeResponse.Message = string.Format("Sucsses Put for Site Employee = {0} ", request.EmployeeId);

            if (request.PhoneNumber != null)
            {
                //update phone
                PhoneMail phoneMail = new PhoneMail();
                phoneMail.PhoneNumber = request.PhoneNumber;
                phoneMail.Email = request.Email;
                phoneMail.EntityTypeId = (int)EntityTypeEnum.Employee;
                phoneMail.EntityId = employeeResponse.Model.EmployeeId;

           var res=   await  EmployeeService.UpdatePhoneMailAsync(phoneMail, typeof(Employee));
                if (res.DIdError)
                    employeeResponse.SetMessageErrorUpdate(nameof(PhoneMail), res.Message);

            }

            if (request.ProffesionTypeId != null)
            {
                EmployeeProffesionType proffesionType = new EmployeeProffesionType();
                proffesionType.ProffesionTypeId = request.ProffesionTypeId;
                proffesionType.EmployeeId = employeeResponse.Model.EmployeeId;

            var res=  await  EmployeeService.UpdateProffesionTypeAsync(proffesionType, typeof(Employee));
                if (res.DIdError)
                    employeeResponse.SetMessageErrorUpdate(nameof(ProffesionType), res.Message);
            }

            //upload picture to blob
            Docs docs = new Docs();
            docs.EntityTypeId = (int)EntityTypeEnum.Employee;
            docs.EntityId = Id;
            var fileName = employee.IdentityNumber + employee.FirstNameEn + "_" + employee.LastNameEn;


            var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee), fileName, request.picture, (int)DocumentType.FaceImage);
            employeeResponse.Model.picturePath = DOCSResponse.Model.DocumentPath;
            if (DOCSResponse.DIdError)
                employeeResponse.SetMessageErrorUpdate("FaceImage", DOCSResponse.Message); 

            DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee), fileName, request.IdentityFile, (int)DocumentType.CopyofID);
            employeeResponse.Model.IdentityFilePath = DOCSResponse.Model.DocumentPath;
            if (DOCSResponse.DIdError)
                employeeResponse.SetMessageErrorUpdate("CopyofID", DOCSResponse.Message); 

            DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(Employee), fileName, request.PassportFile, (int)DocumentType.CopyPassport);
            employeeResponse.Model.PassportFilePath = DOCSResponse.Model.DocumentPath;
            if (DOCSResponse.DIdError)
                employeeResponse.SetMessageErrorUpdate("CopyPassport", DOCSResponse.Message); 


            return employeeResponse.ToHttpResponse();
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

            var entity = await EmployeeService.GetEmployeesAsync(Id);

            if (entity == null)
                return NotFound();

            var response = await EmployeeService.DeleteEmployeeAsync(Id);
            response.SetMessageSucssesDelete(nameof(Employee), Id);

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

            Docs docs = new Docs();
            docs.EntityId = response.Model.EmployeeTrainingId;
            docs.EntityTypeId = (int)EntityTypeEnum.EmployeeTraining;

          
            var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(EmployeeTraining), request.EmployeeTrainingName, request.fileRequest,(int)DocumentType.Training);
            response.Model.Comment = DOCSResponse.Model.DocumentPath;
            if (DOCSResponse.DIdError)
                throw new Exception("Error in create Document EmployeeTraining" + response.Message);

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

            var response = await EmployeeService.CreateEmployeeWorkPermitAsync(entity);

            Docs docs = new Docs();
            docs.EntityId = response.Model.EmployeeWorkPermitId;
            docs.EntityTypeId = (int)EntityTypeEnum.EmployeeWorkPermit;

            var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(EmployeeWorkPermit), request.EmployeeWorkPermitName, request.fileRequest, (int)DocumentType.CopyWorkPermit);
            response.Model.Comment = DOCSResponse.Model.DocumentPath;
            if (DOCSResponse.DIdError)
                throw new Exception("Error in create Document EmployeeWorkPermit" + response.Message);

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

            Docs docs = new Docs();
            docs.EntityId = response.Model.EmployeeAuthorizationId;
            docs.EntityTypeId = (int)EntityTypeEnum.EmployeeAuthtorization;

            var DOCSResponse = await EmployeeService.CreateDocsAsync(docs, typeof(EmployeeAuthtorization), request.EmployeeAuthorizationName, request.fileRequest, (int)DocumentType.Authtorization);
            response.Model.Comment = DOCSResponse.Model.DocumentPath;
            if (DOCSResponse.DIdError)
                throw new Exception("Error in create Document EmployeeAuthtorization" + response.Message);

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
        public async Task<IActionResult> PostEmployeeNoteAsync([FromBody]NoteRequest request)
        {
            var entity = request;

            entity.EntityTypeId = (int)EntityTypeEnum.Employee;
            entity.EntityId = request.EmployeeId;
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