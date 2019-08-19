using System;
using System.Collections.Generic;


namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class EnteryRules
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        public string RuleDescription { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }
    }
}
