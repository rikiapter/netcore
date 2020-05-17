using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class GenTextSystem : IAuditableEntity
    { 
        public int TextSystemID { get; set; }
        public int? TextSystemTypeID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? OrganizationID { get; set; }

        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

     
    }
}
