﻿
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.Core.DAL;


namespace Malam.Mastpen.Core.BL.Services
{
    public class EmployeeService : Service, IEmployeeService
    {
        public EmployeeService(IUserInfo userInfo, MastpenBitachonDbContext dbContext)
            : base( userInfo, dbContext)
        {
        }
        public async Task<IPagedResponse<Employee>> GetEmployeesAsync(int pageSize = 10, int pageNumber = 1, int? EmployeeId = null ,string EmployeeName = null,int? IdentityNumber=null)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {

            var response = new PagedResponse<Employee>();

            // Get the "proposed" query from repository
            var query =DbContext.GetEmployee(EmployeeId,  EmployeeName ,  IdentityNumber);// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה

            // Set paging values
            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = await query.CountAsync();

            // Get the specific page from database
            // response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

            response.SetMessagePages(nameof(GetEmployeesAsync), pageNumber, response.PageCount, response.ItemsCount);

            response.Model = await query
            .Paging(pageSize, pageNumber)
            .ToListAsync();

            // throw new NotImplementedException();
            return response;
        }


        public async Task<SingleResponse<Employee>> GetEmployeeAsync(int Id)
        {
            var response = new SingleResponse<Core.DAL.Entities.Employee>();
            // Get the Employee by Id
            response.Model = await DbContext.GetEmployeesAsync(new Core.DAL.Entities.Employee { EmployeeId = Id });

            response.SetMessageGetById(nameof(GetEmployeeAsync), Id);
            return response;
        }
        public async Task<SingleResponse<Employee>> GetEmployeeByEmployeeNameAsync(int Id)
        {
            var response = new SingleResponse<Core.DAL.Entities.Employee>();
            // Get the Employee by Id
            response.Model = await DbContext.GetEmployeeByEmployeeNameAsync(new Core.DAL.Entities.Employee { EmployeeId = Id });

            return response;
        }
        
        // POST
        public async Task<SingleResponse<Core.DAL.Entities.Employee>> CreateEmployeeAsync(Employee employee)
        {
            var response = new SingleResponse<Core.DAL.Entities.Employee>();
       
               // Add entity to repository
            DbContext.Add(employee, UserInfo);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            response.Message = string.Format("Sucsses Post for Site Employee = {0} ", employee.EmployeeId);
            // Set the entity to response model
            response.Model = employee;

            return response;
        }

        // PUT
        public async Task<Response> UpdateEmployeeAsync(Employee employee)
        {
            var response = new Response();
        
            // Update entity in repository
            DbContext.Update(employee,UserInfo);

            response.Message = string.Format("Sucsses Put for Site Employee = {0} ", employee.EmployeeId);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            return response;
        }

        // DELETE
        public async Task<Response> DeleteEmployeeAsync(int Id)
        {

            var response = new Response();

            // Get Employee by Id
            var entity = await DbContext.GetEmployeesAsync(new Employee { EmployeeId = Id });

            // Remove entity from repository
            DbContext.Remove(entity);

            response.Message = string.Format("Sucsses Delete Site Employee = {0} ", Id);

            // Delete entity in database
            await DbContext.SaveChangesAsync();

            return response;
        }
    }
}
