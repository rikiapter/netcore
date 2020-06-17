
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
using System.Security.Policy;

namespace Malam.Mastpen.Core.BL.Requests
{
    public class HealthRequest
    {
    }
    public class MainScreenHealthResponse
    {
        public float PresentEmployees { get; set; }
        public int NumberEmployees { get; set; }
        public int NumberEmployeesOnSite { get; set; }
        public int WithoutHealthDeclaration { get; set; }//ללא הצהרת בריאות
        public int WithoutHealthDeclarationOnSite { get; set; }// עובדים נוכחים ללא הצהרת בריאות
        public int EmployeesWithHotBody { get; set; }//חום גבוה
        public int NumberVisitors { get; set; }//מבקרים

        public List<AlertsResponse> ListAlertsResponse { get; set; }//התראות
        public List<EmployeePerHour> ListEmployeePerHour { get; set; }//עובדים לפי שעות עבור מסך הדשבורד
    }

    public class EmployeePerHour
    {
        public DateTime Time { get; set; }
        public int count { get; set; }
    }

    public class EmployeeGuid
    {
        public string Guid { get; set; }
    
        public string PhonNumber { get; set; }
        public string Mail { get; set; }
        public int? EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class HealthDeclarationResponse:HealthDeclaration
    {
       public float heatBody { get; set; }
    }

    public static class ExtensionsHealth
    {

        public static EmployeeGuid ToEntity(this Employee request,PhoneMail phoneMail=null)
=> new EmployeeGuid
{
 Guid=request.Guid,
 PhonNumber = phoneMail != null ? phoneMail.PhoneNumber:"0",
 Mail = phoneMail != null ?  phoneMail.Email:"0",
 EmployeeId = request.EmployeeId,
 FirstName=request.FirstName,
 LastName=request.LastName

    };

        public static EmployeeResponse ToEntity(this HealthDeclaration request)
=> new EmployeeResponse
{
FirstName = request.FullName
};

    }
}

