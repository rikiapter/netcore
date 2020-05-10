
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.HR.Core.BL.Requests;
using IdentityModel;
using Malam.Mastpen.Core.DAL.Dbo;

namespace Malam.Mastpen.Core.BL.Requests
    {
#pragma warning disable CS1591



    public class EmployeeRequest
    {
        public int? EmployeeId { get; set; }
        public int? IdentificationTypeId { get; set; }
        public string IdentityNumber { get; set; }
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
        public string Email { get; set; }
        public int? SiteId { get; set; }
        public int? ProffesionTypeId { get; set; }
        public FileRequest picture { get; set; }
        public FileRequest IdentityFile { get; set; }
        public FileRequest PassportFile { get; set; }

        public bool AgreeOnTheBylaws { get; set; }

    }
    
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public int? IdentificationTypeId { get; set; }
        public string IdentityNumber { get; set; }
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
        public int? SiteId { get; set; }
        public bool isEmployeeEntry { get; set; }
        public Gender Gender { get; set; }
        public IdentificationType IdentificationType { get; set; }
        public Organization Organization { get; set; }
        public Country PassportCountry { get; set; }
        public ProffesionType ProffesionType { get; set; }
 
        public TrainingResponse EmployeeAuthtorization { get; set; }
        public TrainingResponse EmployeeTraining { get; set; }
        public TrainingResponse EmployeeWorkPermit { get; set; }
        public PhoneMail phonMail { get; set; }

        public string picturePath { get; set; }
        public string IdentityFilePath { get; set; }
        public string PassportFilePath { get; set; }
       
        //public Docs docs { get; set; }       

        //public ICollection<EmployeeEntry> EmployeeEntry { get; set; }
        // public ICollection<SiteEmployee> SiteEmployeeEmployee { get; set; }
        public ICollection<SiteEmployee> SiteEmployeeSite { get; set; }
        public EmplyeePicture EmployeePicture { get; set; }
        //public ICollection<SiteRole> SiteRole { get; set; }
        public bool AgreeOnTheBylaws { get; set; }
    }

    public class NoteRequest:Notes
    {
        public int EmployeeId { get; set; }
        public EmployeeResponse userEmployee { get; set; }
        public FileRequest FileRequest { get; set; }
        public string uri { get; set; }
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
    public class EmployeeAuthtorizationRequest: EmployeeAuthtorization
    {
      public  FileRequest fileRequest { get; set; }
        public string uri { get; set; }
    }
    public class EmployeeTrainingRequest : EmployeeTraining
    {
        public FileRequest fileRequest { get; set; }
        public string uri { get; set; }
    }
    public class EmployeeWorkPermitRequest : EmployeeWorkPermit
    {
        public FileRequest fileRequest { get; set; }
        public string uri { get; set; }
    }
    public partial class UsersRequest
    {
        public int? EmployeeId { get; set; }
        public int? OrganizationId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? PasswordChangeDate { get; set; }
        public string Comment { get; set; }
    }
    public class EmployeeTrainingDocResponse
    {
        public int EmployeeId { get; set; }
        public string IdentityNumber { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? OrganizationId { get; set; }
        public int? GenderId { get; set; }
        public int? SiteId { get; set; }
        public string picturePath { get; set; }
        public List< GenTextSystem > ListGenTextSystem { get; set; }


    }
    public static class ExtensionsEmployee
    {
        public static Employee ToEntity(this EmployeeRequest request)
        => new Employee
        {
            EmployeeId = request.EmployeeId ?? 0,

            IdentificationTypeId = request.PassportCountryId == 1?1:2,
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
            Address=request.Address

        };
        public static Employee ToEntity(this Employee request)
       => new Employee
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
           Address = request.Address,
           Gender = request.Gender,
           IdentificationType = request.IdentificationType,
           Organization = request.Organization,
           PassportCountry = request.PassportCountry
         

       };
        public static EmployeeResponse ToEntity(this Employee request, PhoneMail phone, Docs docsFaceImage, Docs docsCopyPassport,Docs docsCopyofID, EquipmenAtSite equipmenAtSite,SiteEmployee siteEmployee ,EmplyeePicture emplyeePicture=null)
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
                Address = request.Address,
                Gender = request.Gender,
                IdentificationType = request.IdentificationType,
                Organization = request.Organization,
                PassportCountry = request.PassportCountry,
                EmployeeTraining = request.EmployeeTraining.Count > 0 ? new TrainingResponse((request.EmployeeTraining.Max(x => x.DateTo).Value.Date - DateTime.Now.Date).Days, "תדריך בטיחות") : new TrainingResponse(0, "תדריך בטיחות"),
                EmployeeAuthtorization = request.EmployeeAuthtorization.Count > 0 ? new TrainingResponse((request.EmployeeAuthtorization.Max(x => x.DateTo).Value.Date - DateTime.Now.Date).Days, "אישור עבודה בגובה"): new TrainingResponse(0, "אישור עבודה בגובה"),
                EmployeeWorkPermit = request.IdentificationTypeId == 1 || request.IdentificationTypeId ==null ? new TrainingResponse(100, "היתר עבודה") :request.EmployeeWorkPermit.Count > 0 ? new TrainingResponse((request.EmployeeWorkPermit.Max(x => x.DateTo).Value.Date - DateTime.Now.Date).Days, "היתר עבודה"): new TrainingResponse(0, "היתר עבודה"),
                phonMail = phone,
                ProffesionType = request.EmployeeProffesionType.Count>0? request.EmployeeProffesionType.First().ProffesionType:null,
                picturePath= docsFaceImage ==null ? null: docsFaceImage.DocumentPath,
                IdentityFilePath = docsCopyofID == null ? null : docsCopyofID.DocumentPath,
                PassportFilePath = docsCopyPassport == null ? null : docsCopyPassport.DocumentPath,
                isEmployeeEntry= equipmenAtSite!=null,//אם יש כניסה לאתר
                SiteId= siteEmployee == null ? null : siteEmployee.SiteId,
                EmployeePicture=emplyeePicture
         

            };

        public static EmployeeTrainingDocResponse ToEntity(this Employee request, int siteId, List<GenTextSystem> ListGenTextSystem)
=> new EmployeeTrainingDocResponse
{
 EmployeeId = request.EmployeeId,

 IdentityNumber = request.IdentityNumber,

 FirstName = request.FirstName,
 LastName = request.LastName,

 OrganizationId = request.OrganizationId,

 GenderId = request.GenderId,



 SiteId = siteId,

 ListGenTextSystem= ListGenTextSystem
};

        public static NoteRequest ToEntity(this Notes request, Employee employee,int EmployeeId, Docs docs=null)
              => new NoteRequest
              {
                  userEmployee=employee !=null? employee.ToEntity(null,null,null,null,null,null):null,
                  EmployeeId= EmployeeId,
                  NoteId = request.NoteId,
                  NoteTypeId = request.NoteTypeId,
                  Site = request.Site,
                  NoteContent = request.NoteContent,
                  uri = docs == null ? null : docs.DocumentPath,
                  //UserInsert = request.UserInsert,
                  //DateInsert = request.DateInsert,
                  //UserUpdate = request.UserUpdate,
                  //DateUpdate = request.DateUpdate,
                  //State = request.State,

              };

        public static EmployeeAuthtorizationRequest ToEntity(this EmployeeAuthtorization request, Docs docs)
          => new EmployeeAuthtorizationRequest
          {
              EmployeeAuthorizationId = request.EmployeeAuthorizationId,
              EmployeeAuthorizationName = request.EmployeeAuthorizationName,
              EmployeeId = request.EmployeeId,
              SiteId = request.SiteId,
              DateFrom = request.DateFrom,
              DateTo = request.DateTo,
              Comment = request.Comment,
              AuthorizationTypeId = request.AuthorizationTypeId,
              AuthorizationType = request.AuthorizationType,

              Employee = request.Employee,
              Site = request.Site,

              uri = docs == null ? null : docs.DocumentPath,
              UserInsert = request.UserInsert,
              DateInsert = request.DateInsert,
              UserUpdate = request.UserUpdate,
              DateUpdate = request.DateUpdate,
              State = request.State,

          };

        public static EmployeeTrainingRequest ToEntity(this EmployeeTraining request, Docs docs)
          => new EmployeeTrainingRequest
          {
              EmployeeTrainingId = request.EmployeeTrainingId,
              EmployeeTrainingName = request.EmployeeTrainingName,
              EmployeeId = request.EmployeeId,
              SiteId = request.SiteId,
              DateFrom = request.DateFrom,
              DateTo = request.DateTo,
              Comment = request.Comment,
              TrainingTypeId=request.TrainingTypeId,
              TrainingType=request.TrainingType,
              Employee = request.Employee,
              Site = request.Site,

              uri = docs==null?null:docs.DocumentPath,
              UserInsert = request.UserInsert,
              DateInsert = request.DateInsert,
              UserUpdate = request.UserUpdate,
              DateUpdate = request.DateUpdate,
              State = request.State,

          };

        public static EmployeeWorkPermitRequest ToEntity(this EmployeeWorkPermit request, Docs docs)
              => new EmployeeWorkPermitRequest
              {
                  EmployeeWorkPermitId = request.EmployeeWorkPermitId,
                  EmployeeWorkPermitName = request.EmployeeWorkPermitName,
                  EmployeeId = request.EmployeeId,
                  SiteId = request.SiteId,
                  IsRequired = request.IsRequired,
                  DateFrom = request.DateFrom,
                  DateTo = request.DateTo,
                  Comment = request.Comment,
                  
                  Employee = request.Employee,
                  Site = request.Site,

                  uri = docs == null ? null : docs.DocumentPath,
                  UserInsert = request.UserInsert,
                  DateInsert = request.DateInsert,
                  UserUpdate = request.UserUpdate,
                  DateUpdate = request.DateUpdate,
                  State = request.State,

              };
        public static Users ToEntity(this UsersRequest request)
       => new Users
       {

           EmployeeId = request.EmployeeId,
           OrganizationId = request.OrganizationId,
           UserName = request.UserName,
           Password = request.Password.ToSha256(),
           IsActive = request.IsActive,
           PasswordChangeDate = request.PasswordChangeDate,
           Comment = request.Comment,

       }; 


    }
#pragma warning restore CS1591
}
