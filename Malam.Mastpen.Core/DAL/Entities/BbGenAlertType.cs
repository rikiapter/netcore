using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class AlertType:IAuditableEntity
    {
        public AlertType()
        {
          //  AlertTypeEntity = new HashSet<AlertTypeEntity>();
            Alerts = new HashSet<Alerts>();
        }

        public int AlertTypeId { get; set; }
        public string AlertTypeName { get; set; }
        public int? AlertValidDate { get; set; }
        public int? ModuleID { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

      //  public ICollection<AlertTypeEntity> AlertTypeEntity { get; set; }
        public ICollection<Alerts> Alerts { get; set; }
    }
}
