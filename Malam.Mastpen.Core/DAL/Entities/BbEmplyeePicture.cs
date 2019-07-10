using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class EmplyeePicture:IAuditableEntity
    {
        public int EmployeePictureId { get; set; }
        public int? EmployeeId { get; set; }
        public int? EmployeePicture { get; set; }
        public string EmployeeFacePrintId { get; set; }
        public int? IsUniqueDoc { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public Employee Employee { get; set; }
    }
}
