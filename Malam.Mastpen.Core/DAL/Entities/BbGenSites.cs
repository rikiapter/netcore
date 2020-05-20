using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Sites
    {
        public Sites()
        {
            EquipmenAtSite = new HashSet<EquipmenAtSite>();
            SiteRole = new HashSet<SiteRole>();
            SiteEmployeeSite = new HashSet<SiteEmployee>();
        }

        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public DateTime? SiteActivityStartDate { get; set; }
        public int? SiteTypeId { get; set; }
        public int? SiteStatusId { get; set; }
        public int? OrganizationId { get; set; }
        public string guid { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Organization Organization { get; set; }
        public SiteStatus SiteStatus { get; set; }
        public SiteType SiteType { get; set; }
        public ICollection<EquipmenAtSite> EquipmenAtSite { get; set; }
        public ICollection<SiteRole> SiteRole { get; set; }
        public ICollection<SiteEmployee> SiteEmployeeSite { get; set; }
    
}


}
