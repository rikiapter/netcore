using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Notes:IAuditableEntity
    {
        public int NoteId { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        public int? NoteTypeId { get; set; }
        public int? SiteId { get; set; }
        public string NoteContent { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public EntityType EntityType { get; set; }
        public NoteType NoteType { get; set; }
        public Sites Site { get; set; }
    }
}
