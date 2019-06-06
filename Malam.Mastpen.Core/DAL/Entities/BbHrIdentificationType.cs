using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class IdentificationType
    {
        public IdentificationType()
        {
            Employee = new HashSet<Employee>();
        }

        public int IdentificationTypeId { get; set; }
        public string IdentificationTypeName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}
