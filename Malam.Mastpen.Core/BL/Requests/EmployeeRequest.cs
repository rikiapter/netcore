
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.Core.DAL;

namespace Malam.Mastpen.Core.BL.Requests
    {
#pragma warning disable CS1591



    public class EmployeeRequest
    {
        public int EmployeeId { get; set; }

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
    }



    


    public static class ExtensionsEmployee
    {


        public static Employee ToEntity(this EmployeeRequest request)
        => new Employee
        {
            EmployeeId = request.EmployeeId
        };


    }



#pragma warning restore CS1591
}
