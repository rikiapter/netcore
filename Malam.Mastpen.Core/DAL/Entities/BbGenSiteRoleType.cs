using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class SiteRoleType
    {
        public SiteRoleType()
        {
            SiteRole = new HashSet<SiteRole>();
        }

        public int SiteRoleTypeId { get; set; }
        public string SiteRoleTypeName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<SiteRole> SiteRole { get; set; }
    }
}
