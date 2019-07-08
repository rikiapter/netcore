
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
        public async Task<IPagedResponse<Employee>> GetEmployeesAsync(int pageSize = 10, int pageNumber = 1, int? EmployeeId = null ,string EmployeeName = null,int? IdentityNumber=null, int? OrganizationId = null, int? PassportCountryId = null, int? ProffesionType = null, int? SiteId=null)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {

            var response = new PagedResponse<Employee>();

            // Get the "proposed" query from repository
            var query =DbContext.GetEmployee(EmployeeId,  EmployeeName , IdentityNumber,OrganizationId , PassportCountryId ,ProffesionType, SiteId);// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה

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


        public async Task<SingleResponse<EmployeeResponse>> GetEmployeeAsync(int Id)
        {
            var response = new SingleResponse<EmployeeResponse>();
            // Get the Employee by Id

            // Get query
            var query = DbContext.GetEmployeesAsync(new Employee { EmployeeId = Id });

            // Retrieve items, set model for response
            response.Model = await query.FirstOrDefaultAsync();
            response.Model.ProffesionType.EmployeeProffesionType = null;

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
            var entity = await DbContext.GetEmployeeByEmployeeIdAsync(new Employee { EmployeeId = Id });

            // Remove entity from repository
            DbContext.Remove(entity);

            response.Message = string.Format("Sucsses Delete Site Employee = {0} ", Id);

            // Delete entity in database
            await DbContext.SaveChangesAsync();

            return response;
        }



        // POST
        public async Task<SingleResponse<EmployeeTraining>> CreateEmployeeTrainingAsync(EmployeeTraining employeeTraining)
        {
            var response = new SingleResponse<EmployeeTraining>();

            // Add entity to repository
            DbContext.Add(employeeTraining, UserInfo);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            response.SetMessageSucssesPost(nameof(GetEmployeeAsync), employeeTraining.EmployeeTrainingId);
            // Set the entity to response model
            response.Model = employeeTraining;

            return response;
        }


        // POST
        public async Task<SingleResponse<EmployeeWorkPermit>> CreateEmployeeWorkPermitAsync(EmployeeWorkPermit employeeWorkPermit)
        {
            var response = new SingleResponse<EmployeeWorkPermit>();

            // Add entity to repository
            DbContext.Add(employeeWorkPermit, UserInfo);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            response.SetMessageSucssesPost(nameof(GetEmployeeAsync), employeeWorkPermit.EmployeeWorkPermitId);
            // Set the entity to response model
            response.Model = employeeWorkPermit;

            return response;
        }


        // POST
        public async Task<SingleResponse<EmployeeAuthtorization>> CreateEmployeeAuthtorizationAsync(EmployeeAuthtorization employeeAuthtorization)
        {
            var response = new SingleResponse<EmployeeAuthtorization>();

            // Add entity to repository
            DbContext.Add(employeeAuthtorization, UserInfo);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            response.SetMessageSucssesPost(nameof(GetEmployeeAsync), employeeAuthtorization.EmployeeAuthorizationId);
            // Set the entity to response model
            response.Model = employeeAuthtorization;

            return response;
        }

        // POST
        public async Task<SingleResponse<Notes>> CreateEmployeeNoteAsync(Notes note)
        {
            var response = new SingleResponse<Notes>();

            // Add entity to repository
            DbContext.Add(note, UserInfo);
            // Save entity in database
            await DbContext.SaveChangesAsync();

            response.SetMessageSucssesPost(nameof(GetEmployeeAsync), note.NoteId);
            // Set the entity to response model
            response.Model = note;

            return response;
        }
        //Get List
        public async Task<ListResponse<EmployeeTraining>> GetEmployeeTrainingByEmployeeIdAsync(int Id)
        {
            var response = new ListResponse<EmployeeTraining>();
            // Get the Employee by Id
            var query=  DbContext.GetEmployeeTrainingByEmployeeIdAsync(new EmployeeTraining { EmployeeId = Id });

            response.Model = await query.ToListAsync();

            return response;
        }

        //Get List
        public async Task<ListResponse<EmployeeWorkPermit>> GetEmployeeWorkPermitByEmployeeIdAsync(int Id)
        {
            var response = new ListResponse<EmployeeWorkPermit>();
            // Get the Employee by Id
            var query = DbContext.GetEmployeeWorkPermitByEmployeeIdAsync(new EmployeeWorkPermit { EmployeeId = Id });
            response.Model = await query.ToListAsync();

            return response;
        }

        //Get List
        public async Task<ListResponse<EmployeeAuthtorization>> GetEmployeeAuthtorizationByEmployeeIdAsync(int Id)
        {
            var response = new ListResponse<EmployeeAuthtorization>();
            // Get the Employee by Id
            var query = DbContext.GetEmployeeAuthtorizationByEmployeeIdAsync(new EmployeeAuthtorization { EmployeeId = Id });
            response.Model = await query.ToListAsync();

            return response;
        }
        //Get List
        public async Task<ListResponse<Notes>> GetEmployeeNoteByEmployeeIdAsync(int EmployeeId)
        {
            var response = new ListResponse<Notes>();
            // Get the Employee by Id
            var query = DbContext.GetEmployeeNoteByEmployeeIdAsync(EmployeeId);
            response.Model = await query.ToListAsync();

            return response;
        }
       

    }
}
