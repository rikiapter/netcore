using System;
using System.Collections.Generic;
using System.Text;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.DAL.Entities;

namespace Malam.Mastpen.API.XUnitTest
{
    public static class DbContextExtensions
    {
        public static async void SeedAsync(this MastpenBitachonDbContext dbContext)
        {
            // Add entities for DbContext instance

            dbContext.Employee.Add(new Employee {
                EmployeeId = 1,
                IdentificationTypeId = 2,
                IdentityNumber = 2,
                PassportCountryId = 2,
                FirstName = "FirstName1",
                LastName = "",
                FirstNameEn = "",
                LastNameEn = "",
                OrganizationId = 1,
                BirthDate = new DateTime(2000, 5, 1),
                GenderId = 1,
                Citizenship = 1
            });

            await  dbContext.SaveChangesAsync();

            dbContext.EmployeeProffesionType.Add(new EmployeeProffesionType
            {
                Employee = dbContext.Employee.Find(new object[] { 1 }),
                EmployeeAuthorizationId =1,
                ProffesionTypeId=1,
                Comment ="bla bla"

            });
               await  dbContext.SaveChangesAsync();

            dbContext.EmployeeTraining.Add(new EmployeeTraining
            {
                EmployeeId = 1,
                EmployeeTrainingId = 1,
                TrainingTypeId = 1,
                Comment = "bla bla",
                DateFrom = new DateTime(2000, 5, 1),
                DateTo = new DateTime(2000, 5, 1)

    });
               await  dbContext.SaveChangesAsync();
            dbContext.EmployeeWorkPermit.Add(new EmployeeWorkPermit
            {
                Employee= dbContext.Employee.Find(new object[] { 1 }),
                EmployeeWorkPermitId = 1,
                Comment = "bla bla",
                DateFrom = new DateTime(2000, 5, 1),
                DateTo = new DateTime(2000, 5, 1),
                IsRequired =true

    });
            await dbContext.SaveChangesAsync();
            dbContext.EmployeeAuthtorization.Add(new EmployeeAuthtorization
            {
                Employee = dbContext.Employee.Find(new object[] { 1 }),
                EmployeeAuthorizationId = 1,
                Comment = "bla bla",
                DateFrom = new DateTime(2000, 5, 1),
                DateTo = new DateTime(2000, 5, 1)

            });
            await  dbContext.SaveChangesAsync();
            dbContext.EmplyeePicture.Add(new EmplyeePicture
            {
                EmployeeId = 1,
                EmployeePictureId = 1,
                EmployeePicture=1,
                EmployeeFacePrintId="1",
                IsUniqueDoc=1
            });
               await  dbContext.SaveChangesAsync();
            dbContext.SiteEmployee.Add(new SiteEmployee
            {
                EmployeeId = 1,
                SiteEmployeeId = 1,
                SiteId=1,
                Comment = "bla bla",
                DateFrom = new DateTime(2000, 5, 1),
                DateTo = new DateTime(2000, 5, 1),

            });
               await  dbContext.SaveChangesAsync();
            dbContext.Equipment.Add(new Equipment
            {
                EquipmentId = 1,
                EquipmentTypeId = 1,
                ManufactureName= "ManufactureName",
                ManufactureSerialNumber=1,
                Model= "Model",
                OrganizationId = 1,
                EquipmentStatusTypeId=1,
                Comment = "bla bla",
      

            });
               await  dbContext.SaveChangesAsync();
            dbContext.EquipmenAtSite.Add(new EquipmenAtSite
            {
                EquipmentId = 1,
                EquipmentAtSiteId = 1,
                SiteId=1,
                Comment = "bla bla",
                DateFrom = new DateTime(2000, 5, 1),
                DateTo = new DateTime(2000, 5, 1),


            });
               await  dbContext.SaveChangesAsync();
            dbContext.Address.Add(new Address
            {
                AddressId = 1,
                EntityTypeId = 1,
                EntityId = 1,
                CityId = 1,
                StreetName = "StreetName",
                HouseNumber = 1,
                EntranceNo = "EntranceNo",
                AptNo = "AptNo",
                Pob = 1,
                ZipCode = 1,
                Comments = "bla bla",
                CoorX = "CoorX",
                CoorY = "CoorY"

            });
            dbContext.Address.Add(new Address
            {
                AddressId = 2,
                EntityTypeId = 2,
                EntityId = 1,
                CityId = 1,
                StreetName = "StreetName1",
                HouseNumber = 1,
                EntranceNo = "EntranceNo1",
                AptNo = "AptNo1",
                Pob = 1,
                ZipCode = 1,
                Comments = "bla bla",
                CoorX = "CoorX1",
                CoorY = "CoorY1"

            });
            await  dbContext.SaveChangesAsync();

            dbContext.Docs.Add(new Docs
            {
                DocumentId = 1,
                EntityTypeId = 1,
                EntityId = 1,
                DocumentTypeId = 1,
                DocumentPath = "DocumentPath",
                DocumentDate = new DateTime(2000, 5, 1),
                IsDocumentSigned=true,
                ValIdDate = new DateTime(2000, 5, 1),
                LanguageId = 1

            });
               await  dbContext.SaveChangesAsync();

            dbContext.DocTypeEntity.Add(new DocTypeEntity
            {
                EntityTypeId = 1,
                DocumentTypeId = 1,
                DocumentEntityId=1

            });
               await  dbContext.SaveChangesAsync();
            dbContext.Notes.Add(new Notes
            {
                NoteId = 1,
                EntityTypeId = 1,
                EntityId = 1,
                NoteTypeId = 1,
                NoteContent = "NoteContent"

            });

            dbContext.Organization.Add(new Organization
            {
                OrganizationId = 1,
                OrganizationTypeId = 1,
                OrganizationName = "OrganizationName",
                OrganizationNumber = 1,
                OrganizationParentId=1,
                Comment = "bla bla"

            });
               await  dbContext.SaveChangesAsync();
            dbContext.PhoneMail.Add(new PhoneMail
            {
                ContactId = 1,
                EntityTypeId = 1,
                EntityId=1,
                PhoneTypeId=1,
                PhoneNumber= "PhoneNumber",
                Email = "EMail",

            });

            dbContext.SiteRole.Add(new SiteRole
            {
                SiteRoleId = 1,
                SiteId = 1,
                EmployeeId = 1,
                SiteRoleTypeId = 1,
                Comment = "bla bla",
                DateFrom = new DateTime(2000, 5, 1),
                DateTo = new DateTime(2000, 5, 1),

            });
               await  dbContext.SaveChangesAsync();
            dbContext.Sites.Add(new Sites
            {
                SiteId = 1,
                SiteName = "SiteName",
                SiteTypeId = 1,
                SiteStatusId = 1,
                OrganizationId=1,
                Comment = "bla bla",
                SiteActivityStartDate = new DateTime(2000, 5, 1),
            });

            dbContext.EntityType.Add(new EntityType
            {
                EntityTypeId=1,
                EntityTypeName= "BB_GEN_Sites"
            });
            dbContext.EntityType.Add(new EntityType
            {
                EntityTypeId = 2,
                EntityTypeName = "BB_HR_Employee"
            });

            await  dbContext.SaveChangesAsync();
        }
    }
}