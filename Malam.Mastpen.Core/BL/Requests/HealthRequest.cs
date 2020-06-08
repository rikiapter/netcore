using Malam.Mastpen.Core.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Malam.Mastpen.Core.BL.Requests
{
    public class HealthRequest
    {
    }
    public class MainScreenHealthResponse
    {
        public int PresentEmployees { get; set; }
        public int NumberEmployees { get; set; }
        public int NumberEmployeesOnSite { get; set; }
        public int WithoutHealthDeclaration { get; set; }//ללא הצהרת בריאות
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
 Mail = phoneMail != null ?  phoneMail.Email:"0"

};

    }
}

