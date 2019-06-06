using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class EmployeeProffesionType
    {
        public int EmployeeAuthorizationId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ProffesionTypeId { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Employee Employee { get; set; }
        public ProffesionType ProffesionType { get; set; }
    }
}
