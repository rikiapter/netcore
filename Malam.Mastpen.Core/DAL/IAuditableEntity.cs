using System;
using System.Collections.Generic;
using System.Text;

namespace Malam.Mastpen.Core.DAL
{ 
    public interface IAuditableEntity : IEntity
    {
         int? UserInsert { get; set; }

         DateTime? DateInsert { get; set; }

         int? UserUpdate { get; set; }

         DateTime? DateUpdate { get; set; }

         bool? State { get; set; }


    }
}
