
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.HR.Core.BL.Requests;

namespace Malam.Mastpen.Core.BL.Requests
    {
#pragma warning disable CS1591



    public class EmployeeRequest
    {
        public int? EmployeeId { get; set; }

        public int? IdentificationTypeId { get; set; }

        public int? IdentityNumber { get; set; }

        public int? PassportCountryId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FirstNameEN { get; set; }

        public string LastNameEN { get; set; }

        public int? OrganizationId { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? GenderId { get; set; }

        public int? Citizenship { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public FileRequest picture { get; set; }

        public FileRequest IdentityFile { get; set; }

    }

    public class EmployeeInListResponse
    {
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
        public string Address { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Gender Gender { get; set; }
        public IdentificationType IdentificationType { get; set; }
        public Organization Organization { get; set; }
        public Country PassportCountry { get; set; }

    }
    public class EmployeeResponse
    {
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
        public string Address { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Gender Gender { get; set; }
        public IdentificationType IdentificationType { get; set; }
        public Organization Organization { get; set; }
        public Country PassportCountry { get; set; }
        public ProffesionType ProffesionType { get; set; }
 
        public TrainingResponse EmployeeAuthtorization { get; set; }
        public TrainingResponse EmployeeTraining { get; set; }
        public TrainingResponse EmployeeWorkPermit { get; set; }
        public PhoneMail phonMail { get; set; }

        public Docs docs { get; set; }       
        
        //public ICollection<EmployeeEntry> EmployeeEntry { get; set; }
        //public ICollection<SiteEmployee> SiteEmployeeEmployee { get; set; }
        //public ICollection<SiteEmployee> SiteEmployeeSite { get; set; }
        //public ICollection<EmplyeePicture> EmplyeePicture { get; set; }
        //public ICollection<SiteRole> SiteRole { get; set; }
    }
    public class TrainingResponse
    {
        public TrainingResponse(int regular, string TrainingName)
        {
            this.regular = regular;
            this.TrainingName = TrainingName;
        }

        public int regular { get; set; }
        //במקום הסמכות – אישור עבודה בגובה
        //במקום הדרכות – תדריך בטיחות
        //במקום היתרים – היתר הדרכה
        public string TrainingName { get; set; }
    }


    public static class ExtensionsEmployee
    {


        public static Employee ToEntity(this EmployeeRequest request)
        => new Employee
        {
            EmployeeId = request.EmployeeId ?? 0,

             IdentificationTypeId = request.IdentificationTypeId,
            IdentityNumber = request.IdentityNumber,
            PassportCountryId = request.PassportCountryId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            FirstNameEn = request.FirstNameEN,
            LastNameEn = request.LastNameEN,
            OrganizationId = request.OrganizationId,
            BirthDate = request.BirthDate,
            GenderId = request.GenderId,
            Citizenship = request.Citizenship,

        };
        public static EmployeeInListResponse ToEntity(this Employee request)
     => new EmployeeInListResponse
     {
         EmployeeId = request.EmployeeId,

         IdentificationTypeId = request.IdentificationTypeId,
         IdentityNumber = request.IdentityNumber,
         PassportCountryId = request.PassportCountryId,
         FirstName = request.FirstName,
         LastName = request.LastName,
         FirstNameEn = request.FirstNameEn,
         LastNameEn = request.LastNameEn,
         OrganizationId = request.OrganizationId,
         BirthDate = request.BirthDate,
         GenderId = request.GenderId,
         Citizenship = request.Citizenship,
         UserInsert = request.UserInsert,
         DateInsert = request.DateInsert,
         UserUpdate = request.UserUpdate,
         DateUpdate = request.DateUpdate,
         State = request.State,

         Gender = request.Gender,
         IdentificationType = request.IdentificationType,
         Organization = request.Organization,
         PassportCountry = request.PassportCountry


     };
        public static EmployeeResponse ToEntity(this Employee request, PhoneMail phone,Docs docs)
            => new EmployeeResponse
            {
                EmployeeId = request.EmployeeId,

                IdentificationTypeId = request.IdentificationTypeId,
                IdentityNumber = request.IdentityNumber,
                PassportCountryId = request.PassportCountryId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FirstNameEn = request.FirstNameEn,
                LastNameEn = request.LastNameEn,
                OrganizationId = request.OrganizationId,
                BirthDate = request.BirthDate,
                GenderId = request.GenderId,
                Citizenship = request.Citizenship,
                UserInsert = request.UserInsert,
                DateInsert = request.DateInsert,
                UserUpdate = request.UserUpdate,
                DateUpdate = request.DateUpdate,
                State = request.State,
                Address=request.Address,
                Gender = request.Gender,
                IdentificationType = request.IdentificationType,
                Organization = request.Organization,
                PassportCountry = request.PassportCountry,
                //EmployeeEntry = request.EmployeeEntry,
                //SiteEmployeeEmployee = request.SiteEmployeeEmployee,
                //SiteEmployeeSite = request.SiteEmployeeSite,
                //EmplyeePicture = request.EmplyeePicture,
                //SiteRole = request.SiteRole,
                //EmployeeAuthtorization = request.EmployeeAuthtorization,
                 // EmployeeProffesionType = request.EmployeeProffesionType,
                EmployeeTraining = new TrainingResponse((request.EmployeeTraining.First().DateTo.Value.Date- DateTime.Now.Date).Days , "היתר הדרכה" ),
                EmployeeAuthtorization = new TrainingResponse((request.EmployeeAuthtorization.First().DateTo.Value.Date-DateTime.Now.Date).Days, "תדריך בטיחות"),
                EmployeeWorkPermit = new TrainingResponse(( request.EmployeeWorkPermit.First().DateTo.Value.Date- DateTime.Now.Date ).Days, "אישור עבודה בגובה"),
                //EmployeeWorkPermit = request.EmployeeWorkPermit,
                phonMail = phone,
                ProffesionType=request.EmployeeProffesionType.First().ProffesionType,
               
              //  note=notes,
              //  address=address,
                docs=docs

            };

    }



#pragma warning restore CS1591
}
