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
using IdentityModel.Client;
using System.Net.Http;
using System.Security.Policy;

namespace Malam.Mastpen.Core.BL.Services
{
    public class EmployeeService : Service
    {

        public EmployeeService(IUserInfo userInfo, MastpenBitachonDbContext dbContext, BlobStorageService blobStorageService)
            : base( userInfo, dbContext, blobStorageService)
        {
           
        }


        public async Task<IPagedResponse<EmployeeResponse>> GetEmployeesAsync(int pageSize = 10, int pageNumber = 1, int? EmployeeId = null ,string EmployeeName = null,string IdentityNumber=null, int? OrganizationId = null, int? PassportCountryId = null, int? ProffesionType = null, int? SiteId=null,int? EmployeeIsNotInSiteId=null, bool isEmployeeEntry= false    ,  bool sortByAuthtorization = false,
            bool sortByTraining = false,
            bool sortByWorkPermit = false)//, int? SiteId = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            var response = new PagedResponse<EmployeeResponse>();

            // Get the "proposed" query from repository
            var query =DbContext.GetEmployee(EmployeeId,  EmployeeName , IdentityNumber,OrganizationId , PassportCountryId ,ProffesionType, SiteId, EmployeeIsNotInSiteId, isEmployeeEntry      , sortByAuthtorization , sortByTraining ,  sortByWorkPermit );// אם רוצים לפי סינונים מסוימים אז יש להשתמש בפונקציה

            // Set paging values

            // response.ItemsCount = await query.CountAsync();

            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = await query.CountAsync();


            response.Model = await query
            .Paging(pageSize, pageNumber)
            .ToListAsync();


            //Distinct
            response.Model = response.Model.GroupBy(s => s.EmployeeId)
                                                 .Select(grp => grp.FirstOrDefault())
                                                 .ToList();

            //סינון לפי אב
            response.Model = response.Model.OrderBy(x => x.FirstName);

            //סינון לפי הדרכות וכו
            response.Model= sortByAuthtorization ? response.Model.OrderBy(x => x.EmployeeAuthtorization.regular): response.Model;
            response.Model = sortByWorkPermit ? response.Model.OrderBy(x => x.EmployeeWorkPermit.regular) : response.Model;
            response.Model = sortByTraining ? response.Model.OrderBy(x => x.EmployeeTraining.regular) : response.Model;
           
            //מי נמצא כרגע באתר
            response.Model = isEmployeeEntry && SiteId.HasValue ? response.Model.OrderByDescending(x => x.isEmployeeEntry) : response.Model;

            //מי משויך לאתר
            response.Model = SiteId.HasValue ? response.Model.Where(x => x.SiteId == SiteId) : response.Model;


            var rr = response.Model;
            //מי לא משויך לאתר
            if (EmployeeIsNotInSiteId.HasValue)
            {
                rr = response.Model.Where(x => x.SiteId == EmployeeIsNotInSiteId);

                foreach (var model in rr)
                    response.Model = response.Model.Where(x => x.EmployeeId != model.EmployeeId);
       //             response.Model = EmployeeIsNotInSiteId.HasValue ? response.Model.Where(x => x.SiteId != EmployeeIsNotInSiteId) : response.Model;
            }

 
            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.ItemsCount = response.Model.Count();
            response.SetMessagePages(nameof(GetEmployeesAsync), pageNumber, response.PageCount, response.ItemsCount);
            // throw new NotImplementedException();
            return response;
        }


        public async Task<SingleResponse<EmployeeResponse>> GetEmployeeAsync(int Id)
        {
            var response = new SingleResponse<EmployeeResponse>();

            var query = DbContext.GetEmployeesAsync(new Employee { EmployeeId = Id });

            response.Model = await query.FirstOrDefaultAsync();

            response.SetMessageGetById(nameof(GetEmployeeAsync), Id);
            response.Model.AgreeOnTheBylaws = true;
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

        public async Task<SingleResponse<Users>> GetUserByEmployeeIdAsync(int employeeId)
        {
            var response = new SingleResponse<Users>();

            var query = DbContext.GetUserByEmployeeIdAsync(new Users { EmployeeId = employeeId });

            response.Model = await query;

 
            return response;
        }
        public async Task<SingleResponse<EmployeeResponse>> GetUserAsync(string userName)
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
            response.Model = employee.ToEntity(null,null,null,null,null,null);

            return response;
        }


        // POST
        public async Task<SingleResponse<Users>> CreateUserAsync(Users user)
        {
            var response = new SingleResponse<Users>();

            // Add entity to repository
            DbContext.Add(user, UserInfo);

            await DbContext.SaveChangesAsync();

            response.Message = string.Format("Sucsses Post for user = {0} ", user.EmployeeId);
            // Set the entity to response model
            response.Model = user;

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
                entity.IdentificationTypeId = employee.PassportCountryId == 1 ? 1 : 2; 
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
             //   entity.State = employee.State;
                entity.Address = employee.Address;
                entity.Gender = employee.Gender;
              
                DbContext.Update(entity, UserInfo);

            }

            response.Message = string.Format("Sucsses Put for Site Employee = {0} ", employee.EmployeeId);
            // Save entity in database
            response.Model = employee.ToEntity(null, null, null, null,null,null);
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
        public async Task<ListResponse<EmployeeTrainingRequest>> GetEmployeeTrainingByEmployeeIdAsync(int Id,int TrainingTypeId)
        {
            var response = new ListResponse<EmployeeTrainingRequest>();
            // Get the Employee by Id
            var query=  DbContext.GetEmployeeTrainingByEmployeeIdAsync(new EmployeeTraining { EmployeeId = Id , TrainingTypeId = TrainingTypeId });

            response.Model = await query.ToListAsync();

            return response;
        }

        //Get List
        public async Task<ListResponse<EmployeeWorkPermitRequest>> GetEmployeeWorkPermitByEmployeeIdAsync(int Id)
        {
            var response = new ListResponse<EmployeeWorkPermitRequest>();
            // Get the Employee by Id
            var query = DbContext.GetEmployeeWorkPermitByEmployeeIdAsync(new EmployeeWorkPermit { EmployeeId = Id });
            response.Model = await query.ToListAsync();

            return response;
        }

        //Get List
        public async Task<ListResponse<EmployeeAuthtorizationRequest>> GetEmployeeAuthtorizationByEmployeeIdAsync(int Id)
        {
            var response = new ListResponse<EmployeeAuthtorizationRequest>();
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

        public async Task<SingleResponse<Employee>> GetEmployeeByGuidEntry(string guid)
        {
            var response = new SingleResponse<Employee>();

            var query = DbContext.GetEmployeeByGuidEmployeeEntry(guid);

            response.Model = await query.FirstOrDefaultAsync();

            response.SetMessageGetById(nameof(GetEmployeeByGuidEntry), response.Model.EmployeeId);

            return response;
        }

        public async Task<SingleResponse<Employee>> GetEmployeeByGuid(string guid)
        {
            var response = new SingleResponse<Employee>();

            var query = DbContext.GetEmployeeByGuid(guid);

            response.Model = await query.FirstOrDefaultAsync();

            response.SetMessageGetById(nameof(GetEmployeeByGuidEntry), response.Model.EmployeeId);

            return response;
        }
        public async Task<SingleResponse<Sites>> GetSiteByGuid(string guid)
        {
            var response = new SingleResponse<Sites>();

            var query = DbContext.GetSitesByGuid(guid);

            response.Model = await query.FirstOrDefaultAsync();

            response.SetMessageGetById(nameof(GetSiteByGuid), response.Model.SiteId);

            return response;
        }
        public async Task<SingleResponse<EmployeeEntry>> GetEmployeeEntryByGuidEntry(string guid)
        {
            var response = new SingleResponse<EmployeeEntry>();

            var query = DbContext.GetEmployeeEntryByGuid(guid);

            response.Model = await query.FirstOrDefaultAsync();

            response.SetMessageGetById(nameof(GetEmployeeByGuidEntry), response.Model.EmployeeId??0);

            return response;
        }
        //get
        public async Task<SingleResponse<EquipmenAtSite>> GetSiteByEquipmentIdAsync(int EquipmentId)
        {
            var response = new SingleResponse<EquipmenAtSite>();
            // Get the Employee by Id
            var res = DbContext.GetSiteByEquipmentIdAsync(new EquipmenAtSite { EquipmentId = EquipmentId });
            response.Model = await res.FirstOrDefaultAsync();//.OrderByDescending(x => x.EquipmentId).FirstOrDefaultAsync();

            return response;
        }

        //get
        public async Task<SingleResponse<Sites>> GetSitesByEmployeeIdAsync(int EmployeeId)
        {
            var response = new SingleResponse<Sites>();
            // Get the Employee by Id
            var res = DbContext.GetSitesByEmployeeIdAsync(new SiteEmployee { EmployeeId = EmployeeId });
            response.Model = await res;

            return response;
        }
        
    }
}
