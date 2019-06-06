using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Equipment
    {
        public Equipment()
        {
            EmployeeEntry = new HashSet<EmployeeEntry>();
            EquipmenAtSite = new HashSet<EquipmenAtSite>();
        }

        public int EquipmentId { get; set; }
        public int? EquipmentTypeId { get; set; }
        public string ManufactureName { get; set; }
        public int? ManufactureSerialNumber { get; set; }
        public string Model { get; set; }
        public int? OrganizationId { get; set; }
        public int? EquipmentStatusTypeId { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public EquipmentStatusType EquipmentStatusType { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public Organization Organization { get; set; }
        public ICollection<EmployeeEntry> EmployeeEntry { get; set; }
        public ICollection<EquipmenAtSite> EquipmenAtSite { get; set; }
    }
}
