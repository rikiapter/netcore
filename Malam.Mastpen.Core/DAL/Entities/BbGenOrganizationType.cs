using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class OrganizationType
    {
        public OrganizationType()
        {
            //Organization = new HashSet<Organization>();
        }

        public int OrganizationTypeId { get; set; }
        public string OrganizationTypeName { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        //public ICollection<Organization> Organization { get; set; }
    }
}
