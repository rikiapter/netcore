
using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class AlertTypeEntity
    {
        public int AlertEntityId { get; set; }
        public int? AlertTypeId { get; set; }
        public int? EntityTypeId { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public AlertType AlertType { get; set; }
        public EntityType EntityType { get; set; }
    }
}
