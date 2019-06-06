using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class SiteStatus
    {
        public SiteStatus()
        {
            Sites = new HashSet<Sites>();
        }

        public int SiteStatusId { get; set; }
        public string SiteStatusName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<Sites> Sites { get; set; }
    }
}
