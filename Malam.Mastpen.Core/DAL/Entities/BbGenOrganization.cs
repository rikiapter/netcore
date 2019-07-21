using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Organization:IAuditableEntity
    {
        public Organization()
        {
            //Equipment = new HashSet<Equipment>();
            //Sites = new HashSet<Sites>();
            //Employee = new HashSet<Employee>();
        }

        public int OrganizationId { get; set; }
        public int? OrganizationTypeId { get; set; }
        public int? OrganizationExpertiseTypeId { get; set; }
        public string OrganizationName { get; set; }
        public int? OrganizationNumber { get; set; }
        public int? OrganizationParentId { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public OrganizationType OrganizationType { get; set; }
        public OrganizationExpertiseType OrganizationExpertiseType { get; set; }
        //public ICollection<Equipment> Equipment { get; set; }
        //public ICollection<Sites> Sites { get; set; }
        //public ICollection<Employee> Employee { get; set; }
    }
}
