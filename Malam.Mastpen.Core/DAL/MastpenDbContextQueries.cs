using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.Core.DAL.Dbo;
using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Malam.Mastpen.API.Commom.Infrastructure;

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

        public static IQueryable<Employee> GetEmployee(this MastpenBitachonDbContext dbContext, int? EmployeeID = null, string EmployeeName = null, int? IdentityNumber = null)
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
            return query;


        }

        public static  IQueryable<EmployeeResponse> GetEmployeesAsync(this MastpenBitachonDbContext dbContext, Employee entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;

            var query = from Employee in dbContext.Employee.Where(item => item.EmployeeId == entity.EmployeeId)
                        join phonMail in dbContext.PhoneMail.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        on Employee.EmployeeId equals phonMail.EntityId
                        select Employee.ToEntity(phonMail);
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
        /// <summary>
        /// מחזיר רשימת אתרים כולל הכתובות עבור עמוד הבית
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<SiteResponse> GetSitesAsync(this MastpenBitachonDbContext dbContext, Sites entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Sites)).Result;

            var query = await dbContext.Sites
                .Where(s => s.SiteId == entity.SiteId)
                .Join(

                   //מוציא את הכתובת לפי קוד טבלה וקוד רשומה מטבלת אתרים
                   dbContext.Address.Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId),
                   site => site.SiteId,
                   address => address.EntityId,

                   (site, address) =>
                   //מיפוי לאובייקט הרצוי
                   site.ToEntity(address))

                  .FirstOrDefaultAsync();

            return query;

        }

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