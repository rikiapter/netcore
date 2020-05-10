
using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class BbError : IAuditableEntity
    {
        public int ErrorId { get; set; }
        public int? ErrorTypeId { get; set; }
       public string ErrorTitle { get; set; }
        public string ErrorMessage { get; set; }
 
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }


    }
}
