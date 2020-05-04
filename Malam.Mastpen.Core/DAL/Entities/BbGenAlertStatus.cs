
using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class AlertStatus : IAuditableEntity
    {
        public AlertStatus()
        {
            Alerts = new HashSet<Alerts>();
        }

        public int AlertStatusId { get; set; }
        public string AlertStatusName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<Alerts> Alerts { get; set; }
    }
}
