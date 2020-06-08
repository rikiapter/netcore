using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
  public partial class SiteEmployee : IAuditableEntity
    {
        public int SiteEmployeeId { get; set; }
        public int? EmployeeId { get; set; }
        public int? SiteId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Employee Employee { get; set; }
        public Sites Site { get; set; }
    }
}
