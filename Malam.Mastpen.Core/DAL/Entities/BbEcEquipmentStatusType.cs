using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class EquipmentStatusType
    {
        public EquipmentStatusType()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int EquipmentStatusTypeId { get; set; }
        public string EquipmentStatusTypeName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<Equipment> Equipment { get; set; }
    }
}
