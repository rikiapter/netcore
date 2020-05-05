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

        public static IQueryable<SiteEmployee> GetNumberEmployeesAsync(this MastpenBitachonDbContext dbContext, int siteId)
  => dbContext.SiteEmployee.Where(item => item.SiteId == siteId).AsQueryable();


        public static IQueryable<EmployeeEntry> GetNumberEmployeesOnSiteAsync(this MastpenBitachonDbContext dbContext, int siteId, DateTime date)
        {
            var query = from employeeEntry in dbContext.EmployeeEntry
                           .Where(item => item.Date.Value.Date >= date)

                        join equipment in dbContext.EquipmenAtSite.Where(a => a.SiteId == siteId)
                        on employeeEntry.EquipmentId equals equipment.EquipmentId


                        select employeeEntry;

            return query;
        }

        public static IQueryable<Employee> GetWithoutEmployeeTrainingAsync(this MastpenBitachonDbContext dbContext, int siteId, DateTime date, int? employeeId)
        {

            var query = from employeeEntry in dbContext.EmployeeEntry
                       .Where(item => item.Date.Value.Date >= date)
                        join equipment in dbContext.EquipmenAtSite.Where(a => a.SiteId == siteId)
                        on employeeEntry.EquipmentId equals equipment.EquipmentId
                        select employeeEntry.Employee;

            if (employeeId != null)
                query = dbContext.Employee.Where(x => x.EmployeeId == employeeId);

            query = from employee in query

                    join EmployeeTraining in dbContext.EmployeeTraining
                    .Where(b => b.DateFrom <= DateTime.Now && b.DateTo >= DateTime.Now)
                      .Where(b => b.TrainingTypeId == 4)//הצהרת בריאות
                     on employee.EmployeeId equals EmployeeTraining.EmployeeId

                    select employee;

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
            int? EmployeeIsNotInSiteId = null,
            bool isEmployeeEntry=false, 
            bool sortByAuthtorization = false,
            bool sortByTraining = false,
            bool sortByWorkPermit = false
          
            )
        {
            string tableName = GetTableNameByType(dbContext, typeof(Employee)).Result;

            // Get query from DbSet
            var query = from Employee in dbContext.Employee
                 .Include(b => b.IdentificationType)
                 .Include(o => o.Organization)
                 .Include(x => x.EmployeeProffesionType)
                 .ThenInclude(p => p.ProffesionType)
                 .Include(p=>p.PassportCountry)


                 .Include(x => x.EmployeeAuthtorization)
                 .Include(x => x.EmployeeTraining)
                 .Include(x => x.EmployeeWorkPermit)

                 .AsQueryable()

                        join docsFaceImage in dbContext.Docs
                              .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                              .Where(a => a.DocumentTypeId == (int)DocumentType.FaceImage)
                              on Employee.EmployeeId equals docsFaceImage.EntityId into docsFaceImage
                        from x_docsFaceImage in docsFaceImage.DefaultIfEmpty()

                            //מי שנמצא כרגע  באתר
                        join employeeEntry in dbContext.EmployeeEntry.Where(item => item.Date.Value.Date == DateTime.Now.Date)
                         on Employee.EmployeeId equals employeeEntry.EmployeeId into employeeEntry
                        from x_employeeEntry in employeeEntry.DefaultIfEmpty()

                        join equipmenAtSite in dbContext.EquipmenAtSite.Where(a => a.SiteId == SiteId)
                        on x_employeeEntry.EquipmentId equals equipmenAtSite.EquipmentId into equipmenAtSite
                        from x_equipmenAtSite in equipmenAtSite.DefaultIfEmpty()

                        join siteEmployee in dbContext.SiteEmployee
                        on Employee.EmployeeId equals siteEmployee.EmployeeId into siteEmployee
                        from x_siteEmployee in siteEmployee.DefaultIfEmpty()

                        join docsEmplyeePicture in dbContext.EmplyeePicture
                        on Employee.EmployeeId equals docsEmplyeePicture.EmployeeId into docsEmplyeePicture
                        from x_docsEmplyeePicture in docsEmplyeePicture.DefaultIfEmpty()

                        select Employee.ToEntity(null, x_docsFaceImage, null, null, x_equipmenAtSite, x_siteEmployee, x_docsEmplyeePicture);

 

            //עובדים בארגון ולא משויכים לאתר


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
                          .Include(p => p.PassportCountry)

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


                        join docsEmplyeePicture in dbContext.EmplyeePicture
                        on Employee.EmployeeId equals docsEmplyeePicture.EmployeeId into docsEmplyeePicture
                        from x_docsEmplyeePicture in docsEmplyeePicture.DefaultIfEmpty()

                        select Employee.ToEntity(x_phonMail, x_docsFaceImage, x_docsCopyPassport, x_docsCopyofID,null,null, x_docsEmplyeePicture);


            return query;
        }

        public static IQueryable<EmployeeResponse> GetEmployeeByUserIdAsync(this MastpenBitachonDbContext dbContext, Users entity)
        { 
                 var query = from Employee in dbContext.Employee
                        .Include(x => x.Organization)
                        
        
                        join user in dbContext.Users 
                        .Where(item => item.UserName == entity.UserName)
                        on Employee.EmployeeId equals user.EmployeeId

                        select Employee.ToEntity(null,null,null,null,null,null,null);


            return query;
        }
        public static async Task<Users> GetUserByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, Users entity)
=> await dbContext.Users.FirstOrDefaultAsync(item => item.EmployeeId == entity.EmployeeId);

        public static async Task<Employee> GetEmployeeByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, Employee entity)
    => await dbContext.Employee.FirstOrDefaultAsync(item => item.EmployeeId == entity.EmployeeId);
        public static async Task<Employee> GetEmployeeByIdentityNumberAsync(this MastpenBitachonDbContext dbContext, Employee entity)
            => await dbContext.Employee.FirstOrDefaultAsync(item => item.IdentityNumber == entity.IdentityNumber);

        public static async Task<SiteEmployee> GetSitesByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, SiteEmployee entity)
        => await dbContext.SiteEmployee.Include(b => b.Site)
        .FirstOrDefaultAsync(item => item.EmployeeId == entity.EmployeeId);

        public static IQueryable<EquipmenAtSite> GetSiteByEquipmentIdAsync(this MastpenBitachonDbContext dbContext, EquipmenAtSite entity)
=> dbContext.EquipmenAtSite.Where(item => item.EquipmentId == entity.EquipmentId).DefaultIfEmpty();


        public static IQueryable<EmployeeEntry> GetEmployeeEntryByGuid(this MastpenBitachonDbContext dbContext, string guid)
=> dbContext.EmployeeEntry.Where(item => item.Guid == guid).DefaultIfEmpty();

        public static IQueryable<Employee> GetEmployeeByGuid(this MastpenBitachonDbContext dbContext, string guid)
        {
            var query = from employee in dbContext.Employee

                        join employeeEntry in dbContext.EmployeeEntry
                           .Where(item => item.Guid == guid)
                           on employee.EmployeeId equals employeeEntry.EmployeeId

                        select employee;

            return query;
        }
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

        public static IQueryable<OrganizationRequest> GetOrganizationsAsync(this MastpenBitachonDbContext dbContext, Organization entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Organization)).Result;


            var query = from organization in dbContext.Organization.Where(item => item.OrganizationId == entity.OrganizationId)

                        join docsLogo in dbContext.Docs
                          .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                          .Where(a => a.DocumentTypeId == (int)DocumentType.Logo)
                          on organization.OrganizationId equals docsLogo.EntityId into docsLogo
                        from x_docsLogo in docsLogo.DefaultIfEmpty()

                        join phonMail in dbContext.PhoneMail
                          .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                          on organization.OrganizationId equals phonMail.EntityId into phonMail
                        from x_phonMail in phonMail.DefaultIfEmpty()



                        select organization.ToEntity(x_phonMail, x_docsLogo);

            return query;
        }

        public static IQueryable<OrganizationRequest> GetOrganization(this MastpenBitachonDbContext dbContext, int? OrganizationID = null, string OrganizationName = null, int? OrganizationNumber = null, int? OrganizationExpertiseTypeId = null, int? OrganizationParentId = null)
        {
            string tableName = GetTableNameByType(dbContext, typeof(Organization)).Result;
            // Get query from DbSet
            var query =from organization in dbContext.Organization
                .Include(x => x.OrganizationExpertiseType)
                .Include(x => x.OrganizationType)
                .AsQueryable()

                       join docsLogo in dbContext.Docs
                       .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                       .Where(a => a.DocumentTypeId == (int)DocumentType.Logo)
                       on organization.OrganizationId equals docsLogo.EntityId into docsLogo
                       from x_docsLogo in docsLogo.DefaultIfEmpty()

                       join phonMail in dbContext.PhoneMail
                         .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                         on organization.OrganizationId equals phonMail.EntityId into phonMail
                       from x_phonMail in phonMail.DefaultIfEmpty()


                       select organization.ToEntity(x_phonMail, x_docsLogo);


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
            {
                query = query.Where(item => item.OrganizationParentId == OrganizationParentId || item.OrganizationId == OrganizationParentId);
            
            }
            return query;


        }


        public static IQueryable<EmployeeTrainingRequest> GetEmployeeTrainingByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeTraining entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(EmployeeTraining)).Result;

            // Get query from DbSet
            var query = from tr in dbContext.EmployeeTraining
                .Include(x => x.TrainingType).Include(x => x.Site)
                .Where(item => item.EmployeeId == entity.EmployeeId)
                  .Where(item => item.TrainingTypeId == entity.TrainingTypeId)
                .AsQueryable()

                        join docs in dbContext.Docs
                        .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        .Where(a => a.DocumentTypeId == (int)DocumentType.Training)
                        on tr.EmployeeTrainingId equals docs.EntityId into docs
                        from x_docs in docs.DefaultIfEmpty()

                        select tr.ToEntity(x_docs);
            return query;
        }
        public static IQueryable<EmployeeWorkPermitRequest> GetEmployeeWorkPermitByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeWorkPermit entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(EmployeeWorkPermit)).Result;

            // Get query from DbSet
            var query = from tr in dbContext.EmployeeWorkPermit
               .Include(x => x.Site)
                .Where(item => item.EmployeeId == entity.EmployeeId)
                .AsQueryable()

                        join docs in dbContext.Docs
                        .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        .Where(a => a.DocumentTypeId == (int)DocumentType.CopyWorkPermit)
                        on tr.EmployeeWorkPermitId equals docs.EntityId into docs
                        from x_docs in docs.DefaultIfEmpty()

                        select tr.ToEntity(x_docs);
            return query;
        }

       // => dbContext.EmployeeWorkPermit.AsQueryable().Where(item => item.EmployeeId == entity.EmployeeId).Include(x => x.Site);

        public static IQueryable<EmployeeAuthtorizationRequest> GetEmployeeAuthtorizationByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, EmployeeAuthtorization entity)
        {
            string tableName = GetTableNameByType(dbContext, typeof(EmployeeAuthtorization)).Result;

            // Get query from DbSet
            var query = from tr in dbContext.EmployeeAuthtorization
               .Include(x => x.Site)
               .Include(x=>x.AuthorizationType)
                .Where(item => item.EmployeeId == entity.EmployeeId)
                .AsQueryable()

                        join docs in dbContext.Docs
                        .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableName).EntityTypeId)
                        .Where(a => a.DocumentTypeId == (int)DocumentType.Authtorization)
                        on tr.EmployeeAuthorizationId equals docs.EntityId into docs
                        from x_docs in docs.DefaultIfEmpty()

                        select tr.ToEntity(x_docs);
            return query;
        }
      //  => dbContext.EmployeeAuthtorization.AsQueryable().Where(item => item.EmployeeId == entity.EmployeeId).Include(x => x.Site);

        public static IQueryable<NoteRequest> GetEmployeeNoteByEmployeeIdAsync(this MastpenBitachonDbContext dbContext, int EmployeeId)
        {
            string tableNameNotes = GetTableNameByType(dbContext, typeof(Notes)).Result;
            string tableNameEmployee = GetTableNameByType(dbContext, typeof(Employee)).Result;

            var query = from note in dbContext.Notes
                .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableNameEmployee).EntityTypeId)
                .Where(item => item.EntityId == EmployeeId).Include(x => x.Site).AsQueryable()

             
                        join employee in dbContext.Employee
                        on note.UserInsert equals employee.EmployeeId into employee
                        from x_employee in employee.DefaultIfEmpty()


                        join docs in dbContext.Docs
                        .Where(a => a.EntityTypeId == dbContext.EntityType.FirstOrDefault(item => item.EntityTypeName == tableNameNotes).EntityTypeId)
                        .Where(a => a.DocumentTypeId == (int)DocumentType.Note)
                        on note.NoteId equals docs.EntityId into docs
                        from x_docs in docs.DefaultIfEmpty()

                        select note.ToEntity(x_employee, EmployeeId, x_docs);

            return query;
        }

        public static IQueryable<Alerts> GetAlertsAsync(this MastpenBitachonDbContext dbContext, int siteId,int ModuleID)
   => dbContext.Alerts.Where(item => item.SiteId == siteId)
                      .Where(x => x.Date > DateTime.Now.Date.AddDays(-1))
                      .Where(x=>x.AlertType.ModuleID == ModuleID)
                        .Where(x => x.AlertStatusId == 1)
                      .DefaultIfEmpty()
                      .Include(x => x.AlertType)
                      .Include(x => x.EntityType);

        public static IQueryable<Alerts> GetAlertsAsync(this MastpenBitachonDbContext dbContext,Alerts alerts)
=> dbContext.Alerts.Where(item => item.SiteId ==alerts.SiteId)
                .Where(x => x.Date > DateTime.Now.Date.AddDays(-1))
                .Where(x => x.AlertTypeId == alerts.AlertTypeId)
                 .Where(x => x.EntityId == alerts.EntityId)
                 .Where(x => x.EntityTypeId == alerts.EntityTypeId)
                .DefaultIfEmpty();

        //  select alert.ToEntity(null);

        //todo
        //להוסיף פה גם את הישות
        //id and name

        //    return query;
        //}


        public static IQueryable<Alerts> GetAlerts(this MastpenBitachonDbContext dbContext, int siteId)
         => dbContext.Alerts.Where(item => item.SiteId == siteId);

        public static IQueryable<TrainingDocs> GetTrainingDocs(this MastpenBitachonDbContext dbContext, int? OrganizationId = null, int? LanguageId = null, int? DocumentTypeId = null)
        {
            // Get query from DbSet
            var query = from trainingDocs in dbContext.TrainingDocs.Where(x => x.State == true).AsQueryable()
                        select trainingDocs;
            query = query.Include(x=>x.Language);

            if (OrganizationId.HasValue)
                query = query.Where(item => item.OrganizationId == OrganizationId);



            if (LanguageId.HasValue)
                query = query.Where(item => item.LanguageId == LanguageId);

            if (DocumentTypeId.HasValue)
                query = query.Where(item => item.DocumentTypeId == DocumentTypeId);

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

        public static IQueryable<ParameterCodeEntity> GetEntityTable(this MastpenBitachonDbContext dbContext, string tableName, int entityId)
        {

            if (tableName == GetTableNameByType(dbContext, typeof(Employee)).Result)
            {
                return from entityType in dbContext.Employee.Where(x => x.EmployeeId == entityId)
                       select new ParameterCodeEntity
                       {
                           ParameterFieldID = entityType.EmployeeId,
                           Name = entityType.FirstName + " " + entityType.LastName
                       };
            }
            if (tableName == GetTableNameByType(dbContext, typeof(Organization)).Result)
            {
                return from entityType in dbContext.Organization.Where(x => x.OrganizationId == entityId)
                       select new ParameterCodeEntity
                       {
                           ParameterFieldID = entityType.OrganizationId,
                           Name = entityType.OrganizationName
                       };
            }

            return null;
        }
    }
}