using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class EmployeeHealthCondition : IAuditableEntity
    {
        public int EmployeeHealthConditionId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ConditionTypeId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
