using System;
using System.Collections.Generic;


namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class OrganizationEnteryRules
    {
        public int OrganizationEnteryRulesId { get; set; }
        public int? OrganizationId { get; set; }
        public int? RuleId { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }
    }
}
