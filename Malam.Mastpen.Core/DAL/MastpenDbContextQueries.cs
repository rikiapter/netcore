using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.Core.DAL.Dbo;
using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Malam.Mastpen.API.Commom.Infrastructure;
using Malam.Mastpen.HR.Core.BL.Requests;
using static Malam.Mastpen.API.Commom.Infrastructure.GeneralConsts;

namespace Malam.Mastpen.Core.DAL
{
    public static class MastpenDbContextQueries
    {
        /// <summary>
        /// to do להביא טבלאות מערכת בצורה דינמית
        /// עם codeEntity
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IQueryable<Gender> GetCodeTable(this MastpenBitachonDbContext dbContext, string tableName)
        {
            var query = dbContext.Gender.AsQueryable();
            return query;

        }

        public static IQueryable<EmployeeResponse> GetEmployee(this MastpenBitachonDbContext dbContext, 
            int? EmployeeID = null, 
            string EmployeeName = null, 
            string IdentityNumber = null, 
            int? OrganizationId = null, 
            int? PassportCountryId = null, 
            int? ProffesionType = null, 
            int? SiteId = null, 
            bool isEmployeeEntry=false, 
            bool sortByAuthtorization = false,
            bool sortByTraining = false,
            bool sortByWorkPermit = false)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;

            // Get query from DbSet
            var query = from Employee in dbContext.Employee
                 .Include(b => b.IdentificationType)
                 .Include(o => o.Organization)
                 .Include(x => x.EmployeeProffesionType)
                 .ThenInclude(p => p.ProffesionType)


                 .Include(x => x.EmployeeAuthtorization)
                 .Include(x => x.EmployeeTraining)
                 .Include(x => x.EmployeeWorkPermit)
              //   .Include(x=>x.SiteEmployeeSite)
                 .AsQueryable()

                        join docsFaceImage in dbContext.Docs
                              .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                              .Where(a => a.DocumentTypeId == (int)DocumentType.FaceImage)
                              on Employee.EmployeeId equals docsFaceImage.EntityId into docsFaceImage
                        from x_docsFaceImage in docsFaceImage.DefaultIfEmpty()

                      //  join sites in dbContext.SiteEmployee
                      //  .Where(a => a.SiteId == SiteId  || SiteId ==null || a.SiteId == null)
                      //  on Employee.EmployeeId equals sites.EmployeeId 

            select Employee.ToEntity(null, x_docsFaceImage, null,null);

            if (SiteId.HasValue)
                query = query.Join(
                    dbContext.SiteEmployee.Where(a => a.SiteId == SiteId),
                    siteemployee => siteemployee.EmployeeId,
                    employee => employee.EmployeeId,

                    (employee, siteemployee) => employee);

            // Filter by: 'EmployeeID'
            if (EmployeeID.HasValue)
                query = query.Where(item => item.EmployeeId == EmployeeID);

            if (EmployeeName != null)
                query = query.Where(item => item.FirstName == EmployeeName);

            if (IdentityNumber!=null)
                query = query.Where(item => item.IdentityNumber == IdentityNumber);

            if (OrganizationId.HasValue)
                query = query.Where(item => item.OrganizationId == OrganizationId);

            if (PassportCountryId.HasValue)
                query = query.Where(item => item.PassportCountryId == PassportCountryId);


       
           //מי נמצא כרגע באתר
           // איך אני מכניסה את הערך לאוביקט שחוזר??
            if (isEmployeeEntry && SiteId .HasValue)
                query = query.Join(dbContext.EmployeeEntry
                                    .Where(item => item.Date.Value.Date == DateTime.Now.Date)
                                    .Join
                                     (dbContext.EquipmenAtSite.Where(a => a.SiteId == SiteId),
                                      employeeentry => employeeentry.EquipmentId,
                                      AtSite => AtSite.EquipmentId,

                                     (AtSite, employeeentry) => AtSite),
                 
                          employeeentry1 => employeeentry1.EmployeeId,
                          employee => employee.EmployeeId,

                          (employee, employeeentry1) => employee) ;

   


            return query;


        }

        public static IQueryable<EmployeeResponse> GetEmployeesAsync(this MastpenBitachonDbContext dbContext, Employee entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;

            var query = from Employee in dbContext.Employee
                        .Where(item => item.EmployeeId == entity.EmployeeId)

                        .Include(x => x.EmployeeProffesionType).ThenInclude(p => p.ProffesionType)
                        .Include(x => x.EmployeeAuthtorization)
                        .Include(x => x.EmployeeTraining)
                        .Include(x => x.EmployeeWorkPermit)

                        join phonMail in dbContext.PhoneMail
                        .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on Employee.EmployeeId equals phonMail.EntityId into phonMail
                        from x_phonMail in phonMail.DefaultIfEmpty()

                        join docsFaceImage in dbContext.Docs
                        .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        .Where(a=>a.DocumentTypeId== (int)DocumentType.FaceImage)
                        on Employee.EmployeeId equals docsFaceImage.EntityId into docsFaceImage
                        from x_docsFaceImage in docsFaceImage.DefaultIfEmpty()

                        join docsCopyofID in dbContext.Docs
                       .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                       .Where(a => a.DocumentTypeId == (int)DocumentType.CopyofID)
                        on Employee.EmployeeId equals docsCopyofID.EntityId into docsCopyofID
                        from x_docsCopyofID in docsCopyofID.DefaultIfEmpty()

                        join docsCopyPassport in dbContext.Docs
                        .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        .Where(a => a.DocumentTypeId == (int)DocumentType.CopyPassport)
                        on Employee.EmployeeId equals docsCopyPassport.EntityId into docsCopyPassport
                        from x_docsCopyPassport in docsCopyPassport.DefaultIfEmpty()

                        select Employee.ToEntity(x_phonMail, x_docsFaceImage, x_docsCopyPassport, x_docsCopyofID);


            return query;
        }

        public static IQueryable<EmployeeResponse> GetEmployeeByUserIdAsync(this MastpenBitachonDbContext dbContext, Users entity)
        { 
                 var query = from Employee in dbContext.Employee
                        .Include(x => x.Organization)
                        
        
                        join user in dbContext.Users 
                        .Where(item => item.UserName == entity.UserName)
                        on Employee.EmployeeId equals user.EmployeeId

                        select Employee.ToEntity(null,null,null,null);


            return query;
        }
        public static async Task<Employee> GetEmployeeByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, Employee entity)
    => await dbContext.Employee.FirstOrDefaultAsync(item => item.EmployeeId == entity.EmployeeId);
        public static async Task<Employee> GetEmployeeByIdentityNumberAsync(this MastpenBitachonDbContext dbContext, Employee entity)
            => await dbContext.Employee.FirstOrDefaultAsync(item => item.IdentityNumber == entity.IdentityNumber);

        public static async Task<SiteEmployee> GetSitesByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, SiteEmployee entity)
        => await dbContext.SiteEmployee.Include(b => b.Site)
        .FirstOrDefaultAsync(item => item.EmployeeId == entity.EmployeeId);

        /// <summary>
        /// מקבל קוד טבלה וקוד בתוך הטבלה
        /// לדוגמא 1 טבלת אתרים
        /// 2 הקוד של האתר
        /// ומחזיר את כתובת האתר
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<Address> GetAddressAsync(this MastpenBitachonDbContext dbContext, Address entity)
        => await dbContext.Address.FirstOrDefaultAsync(item => item.EntityTypeId == entity.EntityTypeId && item.EntityId == entity.EntityId);

        public static async Task<Docs> GetDocsAsync(this MastpenBitachonDbContext dbContext, Docs entity)
        => await dbContext.Docs.FirstOrDefaultAsync(item => item.EntityTypeId == entity.EntityTypeId 
                                                    && item.EntityId == entity.EntityId 
                                                    && item.DocumentTypeId==entity.DocumentTypeId);

        public static async Task<PhoneMail> GetPhoneMailAsync(this MastpenBitachonDbContext dbContext, PhoneMail entity)
=> await dbContext.PhoneMail.FirstOrDefaultAsync(item => item.EntityTypeId == entity.EntityTypeId && item.EntityId == entity.EntityId);

        public static async Task<Organization> GetOrganizationeByIdAsync(this MastpenBitachonDbContext dbContext, Organization entity)
            => await dbContext.Organization.FirstOrDefaultAsync(item => item.OrganizationId == entity.OrganizationId);
        public static async Task<Organization> GetOrganizationeByNameAsync(this MastpenBitachonDbContext dbContext, Organization entity)
    => await dbContext.Organization.FirstOrDefaultAsync(item => item.OrganizationName == entity.OrganizationName);

        public static IQueryable<OrganizationResponse> GetOrganizationsAsync(this MastpenBitachonDbContext dbContext, Organization entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Organization)).Result;


            var query = from organization in dbContext.Organization.Where(item => item.OrganizationId == entity.OrganizationId)

                        join phonMail in dbContext.PhoneMail
                          .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                          on organization.OrganizationId equals phonMail.EntityId into phonMail
                        from x_phonMail in phonMail.DefaultIfEmpty()



                        select organization.ToEntity(x_phonMail);

            return query;
        }

        public static IQueryable<Organization> GetOrganization(this MastpenBitachonDbContext dbContext, int? OrganizationID = null, string OrganizationName = null, int? OrganizationNumber = null, int? OrganizationExpertiseTypeId = null,int? OrganizationParentId=null)
        {

            // Get query from DbSet
            var query = dbContext.Organization
                .Include(x=>x.OrganizationExpertiseType)
                .Include(x => x.OrganizationType)
                .AsQueryable();


            // Filter by: 'EmployeeID'
            if (OrganizationID.HasValue)
                query = query.Where(item => item.OrganizationId == OrganizationID);

            if (OrganizationName != null)
                query = query.Where(item => item.OrganizationName == OrganizationName);

            if (OrganizationNumber != null)
                query = query.Where(item => item.OrganizationNumber == OrganizationNumber);

            if (OrganizationExpertiseTypeId != null)
                query = query.Where(item => item.OrganizationExpertiseTypeId == OrganizationExpertiseTypeId);

            if (OrganizationParentId != null)
                query = query.Where(item => item.OrganizationParentId == OrganizationParentId);

            return query;


        }


        public static IQueryable<EmployeeTrainingRequest> GetEmployeeTrainingByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeTraining entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;

            // Get query from DbSet
            var query = from tr in dbContext.EmployeeTraining
                .Include(x => x.TrainingType).Include(x => x.Site)
                .Where(item => item.EmployeeId == entity.EmployeeId)
                .AsQueryable()

                        join docs in dbContext.Docs
                        .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        .Where(a => a.DocumentTypeId == (int)DocumentType.Training)
                        on tr.EmployeeId equals docs.EntityId into docs
                        from x_docs in docs.DefaultIfEmpty()

                        select tr.ToEntity(x_docs);
            return query;
        }
        public static IQueryable<EmployeeWorkPermit> GetEmployeeWorkPermitByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeWorkPermit entity)
=> dbContext.EmployeeWorkPermit.AsQueryable().Where(item => item.EmployeeId == entity.EmployeeId).Include(x => x.Site);

        public static IQueryable<EmployeeAuthtorization> GetEmployeeAuthtorizationByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeAuthtorization entity)
=> dbContext.EmployeeAuthtorization.AsQueryable().Where(item => item.EmployeeId == entity.EmployeeId).Include(x => x.Site);

        public static IQueryable<NoteRequest> GetEmployeeNoteByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, int EmployeeId)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;


            var query = from note in dbContext.Notes
                .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                .Where(item => item.EntityId == EmployeeId).Include(x => x.Site).AsQueryable()

             
                        join employee in dbContext.Employee
                        on note.UserInsert equals employee.EmployeeId into employee
                        from x_employee in employee.DefaultIfEmpty()
                        
                        select note.ToEntity(x_employee, EmployeeId);

            return query;
        }



        /// <summary>
        ///get ntityTypeId
        ///return entity tablee
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<string> GetTableNameByType(this MastpenBitachonDbContext dbContext, Type entity)
        {
            var mapping = dbContext.Model.FindEntityType(entity).Relational();
            var schema = mapping.Schema;
            var tableName = mapping.TableName;

            //   return dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId;
            return tableName;
        }


        /// <summary>
        ///get entity table
        ///return entityTypeId
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<int> GetEntityTypeIdByEntityTypeName(this MastpenBitachonDbContext dbContext, Type entity)
        {
            var mapping = dbContext.Model.FindEntityType(entity).Relational();
            var schema = mapping.Schema;
            var tableName = mapping.TableName;

            return dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId;

        }
    }
}