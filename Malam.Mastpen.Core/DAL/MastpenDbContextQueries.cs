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
            // Get query from DbSet
            var query = dbContext.Gender.AsQueryable();

            //   var query2=dbContext.Where(item => item.TableName == tableName)
            // if (tableName)

            return query;

            //return dbContext.Employee.ToList().AsQueryable();

        }

        public static IQueryable<Employee> GetEmployee(this MastpenBitachonDbContext dbContext, int? EmployeeID = null, string EmployeeName = null, int? IdentityNumber = null, int? OrganizationId = null, int? PassportCountryId = null, int? ProffesionType = null)
        {

            // Get query from DbSet
            var query = dbContext.Employee
                .Include(b => b.IdentificationType)
                .AsQueryable();

            // Filter by: 'EmployeeID'
            if (EmployeeID.HasValue)
                query = query.Where(item => item.EmployeeId == EmployeeID);

            if (EmployeeName!=null)
                query = query.Where(item => item.FirstName == EmployeeName);

            if (IdentityNumber.HasValue)
                query = query.Where(item => item.IdentityNumber == IdentityNumber);

            if (OrganizationId.HasValue)
                query = query.Where(item => item.OrganizationId == OrganizationId);

            if (PassportCountryId.HasValue)
                query = query.Where(item => item.PassportCountryId == PassportCountryId);

            //if (ProffesionType.HasValue)
            //    query = query.Where(item => item.ProffesionType == ProffesionType);

            return query;


        }

        public static  IQueryable<EmployeeResponse> GetEmployeesAsync(this MastpenBitachonDbContext dbContext, Employee entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;


            var query = from Employee in dbContext.Employee.Where(item => item.EmployeeId == entity.EmployeeId)

                        join phonMail in dbContext.PhoneMail.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on Employee.EmployeeId equals phonMail.EntityId

                        join note in dbContext.Notes.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on Employee.EmployeeId equals note.EntityId

                        join address in dbContext.Address.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on Employee.EmployeeId equals address.EntityId

                        join docs in dbContext.Docs.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on Employee.EmployeeId equals docs.EntityId

                        select Employee.ToEntity(phonMail,note,address,docs);
            
            //.Include(b => b.EmplyeePicture)
            //.Include(c => c.EmployeeProffesionType)

        //  .FirstOrDefaultAsync(item => item.EmployeeId == entity.EmployeeId);
        //  return query;

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
        /// <summary>
        ///get entity table
        ///return entityTypeId
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
    }
}