﻿using System;
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
    public class EmployeeController : MastpenController
    {
        protected readonly IRothschildHouseIdentityClient RothschildHouseIdentityClient;
        protected readonly IEmployeeService EmployeeService;
        public EmployeeController(
            IRothschildHouseIdentityClient rothschildHouseIdentityClient,
                 IEmployeeService employeeService)
        {
            RothschildHouseIdentityClient = rothschildHouseIdentityClient;
            EmployeeService = employeeService;
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
        //  [Authorize(Policy = Policies.CustomerPolicy)]
        public async Task<IActionResult> GetEmployeesAsync(int pageSize = 10, int pageNumber = 1, int? EmployeeId = null, string EmployeeName = null, int? IdentityNumber = null)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var response = await EmployeeService.GetEmployeesAsync(pageSize, pageNumber, EmployeeId, EmployeeName ,  IdentityNumber );

            // Return as http response
            return response.ToHttpResponse();
        }

        // GET
        // api/v1/Employee/Employee/5

        /// <summary>
        /// Retrieves a Employee by Id
        /// 1.33.	מסך מספר 2.3 – תיק עובד
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
        [Authorize(Policy = Policies.CustomerPolicy)]
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
            var existingEntity = await EmployeeService.GetEmployeeByEmployeeNameAsync(request.EmployeeId);

            if (existingEntity.Model != null)
                ModelState.AddModelError("EmployeeName", "Employee name already exists");

            if (!ModelState.IsValid)
                return BadRequest();

            var entity = request.ToEntity();

            entity.UserInsert = UserInfo.UserId;

            var response = await EmployeeService.CreateEmployeeAsync(entity);

            return response.ToHttpResponse();
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
            var entity = await EmployeeService.GetEmployeesAsync(request.EmployeeId);
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
    }
}