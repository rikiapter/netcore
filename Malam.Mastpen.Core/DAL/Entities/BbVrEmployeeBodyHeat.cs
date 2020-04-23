using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class EmployeeBodyHeat : IAuditableEntity
    {
        public int EmployeeBodyHeatId { get; set; }
        public int? EmployeeId { get; set; }
        public int? BodyHeat { get; set; }
        public DateTime? DateTest { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
