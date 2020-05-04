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
    }
 
    public static class ExtensionsHealth
    {
     


    }
}