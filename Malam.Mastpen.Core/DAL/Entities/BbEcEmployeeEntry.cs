using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class EmployeeEntry : IAuditableEntity
    {
        public int EmployeeEntryId { get; set; }
        public int? EquipmentId { get; set; }
        public int? EmployeeId { get; set; }
        public int? EntryApprovalType { get; set; }
        public string Guid { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Employee Employee { get; set; }
        public Equipment Equipment { get; set; }
    }
}
