using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Employee: IAuditableEntity
    {
        public Employee()
        {
            EmployeeEntry = new HashSet<EmployeeEntry>();
            SiteEmployeeEmployee = new HashSet<SiteEmployee>();
            SiteEmployeeSite = new HashSet<SiteEmployee>();
            EmplyeePicture = new HashSet<EmplyeePicture>();
            SiteRole = new HashSet<SiteRole>();
            EmployeeAuthtorization = new HashSet<EmployeeAuthtorization>();
            EmployeeProffesionType = new HashSet<EmployeeProffesionType>();
            EmployeeTraining = new HashSet<EmployeeTraining>();
            EmployeeWorkPermit = new HashSet<EmployeeWorkPermit>();
        }

        public int EmployeeId { get; set; }
        public int? IdentificationTypeId { get; set; }
        public int? IdentityNumber { get; set; }
        public int? PassportCountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public int? OrganizationId { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? GenderId { get; set; }
        public int? Citizenship { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Gender Gender { get; set; }
        public IdentificationType IdentificationType { get; set; }
        public Organization Organization { get; set; }
        public Country PassportCountry { get; set; }
        public ICollection<EmployeeEntry> EmployeeEntry { get; set; }
        public ICollection<SiteEmployee> SiteEmployeeEmployee { get; set; }
        public ICollection<SiteEmployee> SiteEmployeeSite { get; set; }
        public ICollection<EmplyeePicture> EmplyeePicture { get; set; }
        public ICollection<SiteRole> SiteRole { get; set; }
        public ICollection<EmployeeAuthtorization> EmployeeAuthtorization { get; set; }
        public ICollection<EmployeeProffesionType> EmployeeProffesionType { get; set; }
        public ICollection<EmployeeTraining> EmployeeTraining { get; set; }
        public ICollection<EmployeeWorkPermit> EmployeeWorkPermit { get; set; }
    }
}
