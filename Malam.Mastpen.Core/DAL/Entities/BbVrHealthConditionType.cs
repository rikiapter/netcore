using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class HealthConditionType : IAuditableEntity
    {
        public int ConditionTypeId { get; set; }
        public string ConditionTypeName { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }
    }
}
