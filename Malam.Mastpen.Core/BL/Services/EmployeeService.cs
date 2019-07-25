
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
using IdentityModel.Client;
using System.Net.Http;

namespace Malam.Mastpen.Core.BL.Services
{
    public class EmployeeService : Service
    {

        public EmployeeService(IUserInfo userInfo, MastpenBitachonDbContext dbContext, BlobStorageService blobStorageService)
            : base( userInfo, dbContext, blobStorageService)
        {
           
        }


        public async Task<IPagedResponse<EmployeeResponse>> GetEmployeesAsync(int pageSize = 10, int pageNumber = 1, int? EmployeeId = null ,string EmployeeName = null,string IdentityNumber=null, int? OrganizationId = null, int? PassportCountryId = null, int? ProffesionType = null, int? SiteId=null)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var response = new PagedResponse<EmployeeResponse>();

            // Get the "proposed" query from repository
            var query =DbContext.GetEmployee(EmployeeId,  EmployeeName , IdentityNumber,OrganizationId , PassportCountryId ,ProffesionType, SiteId);// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה

            // Set paging values
            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = await query.CountAsync();

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

            var query = DbContext.GetEmployeesAsync(new Employee { EmployeeId = Id });

            response.Model = await query.FirstOrDefaultAsync();

            response.SetMessageGetById(nameof(GetEmployeeAsync), Id);
            return response;
        }

        public async Task<SingleResponse<EmployeeResponse>> GetEmployeeByUserIdAsync(string userName)
        {
            var response = new SingleResponse<EmployeeResponse>();

            var query = DbContext.GetEmployeeByUserIdAsync(new Users { UserName = userName });

            response.Model = await query.FirstOrDefaultAsync();

            response.SetMessageGetById(nameof(GetEmployeeByUserIdAsync), response.Model.EmployeeId);
            return response;
        }




        // POST
        public async Task<SingleResponse<EmployeeResponse>> CreateEmployeeAsync(Employee employee)
        {
            var response = new SingleResponse<EmployeeResponse>();
       
            // Add entity to repository
            DbContext.Add(employee, UserInfo);

            await DbContext.SaveChangesAsync();

            response.Message = string.Format("Sucsses Post for Employee = {0} ", employee.EmployeeId);
            // Set the entity to response model
            response.Model = employee.ToEntity(null,null,null,null);

            return response;
        }

        //get
        public async Task<SingleResponse<Employee>> GetEmployeeByIdentityNumberAsync(string identityNumber)
        {
            var response = new SingleResponse<Core.DAL.Entities.Employee>();
            // Get the Employee by Id
            response.Model = await DbContext.GetEmployeeByIdentityNumberAsync(new Core.DAL.Entities.Employee { IdentityNumber = identityNumber });

            return response;
        }
        // POST or update
        public async Task<SingleResponse<EmplyeePicture>> CreateEmplyeePictureAsync(EmplyeePicture emplyeePicture)
        {
            var response = new SingleResponse<EmplyeePicture>();

            EmplyeePicture entity = DbContext.EmplyeePicture.FirstOrDefault(item => item.EmployeeId == emplyeePicture.EmployeeId);

            if (entity != null)
            {
                entity.EmployeeFacePrintId = emplyeePicture.EmployeeFacePrintId;
                DbContext.Update(entity, UserInfo);
            }
            else 
                DbContext.Add(emplyeePicture, UserInfo);

            await DbContext.SaveChangesAsync();

            response.Message = string.Format("Sucsses Post for  emplyeePicture = {0} ", emplyeePicture);

            response.Model = emplyeePicture;

            return response;
        }

        // PUT
        public async Task<SingleResponse<EmployeeResponse>> UpdateEmployeeAsync(Employee employee)
        {
            var response = new SingleResponse<EmployeeResponse>();

            // Retrieve entity by id
            // Answer for question #1
            Employee entity = DbContext.Employee.FirstOrDefault(item => item.EmployeeId == employee.EmployeeId);

            // Validate entity is not null
            if (entity != null)
            {
                //entity = employee.ToEntity();
                entity.IdentificationTypeId = employee.IdentificationTypeId;
                entity.IdentityNumber = employee.IdentityNumber;
                entity.PassportCountryId = employee.PassportCountryId;
                entity.FirstName = employee.FirstName;
                entity.LastName = employee.LastName;
                entity.FirstNameEn = employee.FirstNameEn;
                entity.LastNameEn = employee.LastNameEn;
                entity.OrganizationId = employee.OrganizationId;
                entity.BirthDate = employee.BirthDate;
                entity.GenderId = employee.GenderId;
                entity.Citizenship = employee.Citizenship;
              //  entity.UserInsert = employee.UserInsert;
              //  entity.DateInsert = employee.DateInsert;
                entity.State = employee.State;
                entity.Address = employee.Address;
                entity.Gender = employee.Gender;
                entity.IdentificationType = employee.IdentificationType;
                entity.Organization = employee.Organization;
                entity.PassportCountry = employee.PassportCountry;
              
                DbContext.Update(entity, UserInfo);

            }

            response.Message = string.Format("Sucsses Put for Site Employee = {0} ", employee.EmployeeId);
            // Save entity in database
            response.Model = employee.ToEntity(null, null, null, null);
            await DbContext.SaveChangesAsync();

            return response;
        }

        // DELETE
        public async Task<ResponseBasic> DeleteEmployeeAsync(int Id)
        {
            var response = new ResponseBasic();

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


            DbContext.Add(employeeTraining, UserInfo);

            await DbContext.SaveChangesAsync();

            response.Model = employeeTraining;

            return response;
        }


        // POST
        public async Task<SingleResponse<EmployeeWorkPermit>> CreateEmployeeWorkPermitAsync(EmployeeWorkPermit employeeWorkPermit)
        {
            var response = new SingleResponse<EmployeeWorkPermit>();

            DbContext.Add(employeeWorkPermit, UserInfo);
            await DbContext.SaveChangesAsync();

            response.Model = employeeWorkPermit;

            return response;
        }


        // POST
        public async Task<SingleResponse<EmployeeAuthtorization>> CreateEmployeeAuthtorizationAsync(EmployeeAuthtorization employeeAuthtorization)
        {
            var response = new SingleResponse<EmployeeAuthtorization>();

            DbContext.Add(employeeAuthtorization, UserInfo);

            await DbContext.SaveChangesAsync();

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
        public async Task<ListResponse<NoteRequest>> GetEmployeeNoteByEmployeeIdAsync(int EmployeeId)
        {
            var response = new ListResponse<NoteRequest>();
            // Get the Employee by Id
            var query = DbContext.GetEmployeeNoteByEmployeeIdAsync(EmployeeId);
            response.Model = await query.ToListAsync();

            return response;
        }
       

    }
}
