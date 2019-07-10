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

        public static IQueryable<Employee> GetEmployee(this MastpenBitachonDbContext dbContext, int? EmployeeID = null, string EmployeeName = null, int? IdentityNumber = null, int? OrganizationId = null, int? PassportCountryId = null, int? ProffesionType = null,int ? SiteId=null)
        {

            // Get query from DbSet
          var   query = dbContext.Employee
                .Include(b => b.IdentificationType)
                .Include(o => o.Organization)
                .Include(x => x.EmployeeProffesionType).ThenInclude(p => p.ProffesionType)
                .Include(x => x.EmployeeAuthtorization)
                .Include(x => x.EmployeeTraining)
                .Include(x => x.EmployeeWorkPermit)
                .AsQueryable();

            // Filter by: 'EmployeeID'
            if (EmployeeID.HasValue)
                query = query.Where(item => item.EmployeeId == EmployeeID);

            if (EmployeeName != null)
                query = query.Where(item => item.FirstName == EmployeeName);

            if (IdentityNumber.HasValue)
                query = query.Where(item => item.IdentityNumber == IdentityNumber);

            if (OrganizationId.HasValue)
                query = query.Where(item => item.OrganizationId == OrganizationId);

            if (PassportCountryId.HasValue)
                query = query.Where(item => item.PassportCountryId == PassportCountryId);

            if (SiteId.HasValue)
                query = query.Where(item => item.SiteEmployeeSite.Any(siteid=> siteid.SiteId==SiteId));


            return query;


        }

        public static  IQueryable<EmployeeResponse> GetEmployeesAsync(this MastpenBitachonDbContext dbContext, Employee entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;


            var query = from Employee in dbContext.Employee    
                        .Where(item => item.EmployeeId == entity.EmployeeId)
   
                        .Include(x => x.EmployeeProffesionType).ThenInclude(p=>p.ProffesionType)
                        .Include(x => x.EmployeeAuthtorization)
                        .Include(x => x.EmployeeTraining)
                        .Include(x => x.EmployeeWorkPermit)

                        join phonMail in dbContext.PhoneMail.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on Employee.EmployeeId equals phonMail.EntityId into phonMail
                        from x_phonMail in phonMail.DefaultIfEmpty()


                        join docs in dbContext.Docs.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on Employee.EmployeeId equals docs.EntityId into docs
                        from x_docs in docs.DefaultIfEmpty()

                        select Employee.ToEntity( x_phonMail,x_docs);


            return query;
        }
        public static async Task<Employee> GetEmployeeByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, Employee entity)
    => await dbContext.Employee.FirstOrDefaultAsync(item => item.EmployeeId == entity.EmployeeId);
        public static async Task<Employee> GetEmployeeByEmployeeNameAsync(this MastpenBitachonDbContext dbContext, Employee entity)
            => await dbContext.Employee.FirstOrDefaultAsync(item => item.EmployeeId == entity.EmployeeId);

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

        public static async Task<Organization> GetOrganizationeByIdAsync(this MastpenBitachonDbContext dbContext, Organization entity)
            => await dbContext.Organization.FirstOrDefaultAsync(item => item.OrganizationId == entity.OrganizationId);

        public static IQueryable<OrganizationResponse> GetOrganizationsAsync(this MastpenBitachonDbContext dbContext, Organization entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Organization)).Result;


            var query = from organization in dbContext.Organization.Where(item => item.OrganizationId == entity.OrganizationId)

                        join phonMail in dbContext.PhoneMail.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on organization.OrganizationId equals phonMail.EntityId


                        select organization.ToEntity(phonMail);

            return query;
        }

        public static IQueryable<Organization> GetOrganization(this MastpenBitachonDbContext dbContext, int? OrganizationID = null, string OrganizationName = null)
        {

            // Get query from DbSet
            var query = dbContext.Organization .AsQueryable();

            // Filter by: 'EmployeeID'
            if (OrganizationID.HasValue)
                query = query.Where(item => item.OrganizationId == OrganizationID);

            if (OrganizationName != null)
                query = query.Where(item => item.OrganizationName == OrganizationName);

            return query;


        }


        public static IQueryable<EmployeeTraining> GetEmployeeTrainingByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeTraining entity)
        {
     
            // Get query from DbSet
            var query = dbContext.EmployeeTraining
                .Include(x => x.TrainingType).Include(x=>x.Site)
                .Where(item => item.EmployeeId == entity.EmployeeId)
                .AsQueryable();

            return query;
        }
        public static IQueryable<EmployeeWorkPermit> GetEmployeeWorkPermitByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeWorkPermit entity)
=>  dbContext.EmployeeWorkPermit.AsQueryable().Where(item => item.EmployeeId == entity.EmployeeId).Include(x => x.Site);

        public static  IQueryable< EmployeeAuthtorization> GetEmployeeAuthtorizationByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeAuthtorization entity)
=>  dbContext.EmployeeAuthtorization.AsQueryable().Where(item => item.EmployeeId == entity.EmployeeId).Include(x => x.Site);

        public static IQueryable<NoteResponse> GetEmployeeNoteByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, int EmployeeId)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;


            var query = from note in dbContext.Notes
                .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                .Where(item => item.EntityId == EmployeeId).Include(x => x.Site).AsQueryable()

                        join employee in dbContext.Employee
                        on note.UserInsert equals employee.EmployeeId


                        select note.ToEntity(employee);

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