using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.Core.DAL.Entities;
using System;
using System.Collections.Generic;

namespace Malam.Mastpen.HR.Core.DAL.Entities
{
    public partial class Users : IAuditableEntity
    {
        public int UserID { get; set; }
        public int? OrganizationID { get; set; }
        public int? EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? PasswordChangeDate { get; set; }
        public string Comment { get; set; }

        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Employee Employee { get; set; }
        public Organization Organization { get; set; }

    }
}
