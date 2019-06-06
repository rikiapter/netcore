using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class SiteType
    {
        public SiteType()
        {
            Sites = new HashSet<Sites>();
        }

        public int SiteTypeId { get; set; }
        public string SiteTypeName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<Sites> Sites { get; set; }
    }
}
