using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class ProffesionType:IAuditableEntity
    {
        public ProffesionType()
        {
         //   EmployeeProffesionType = new HashSet<EmployeeProffesionType>();
        }

        public int ProffesionTypeId { get; set; }
        public string ProffesionTypeName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

      //  public ICollection<EmployeeProffesionType> EmployeeProffesionType { get; set; }
    }
}
