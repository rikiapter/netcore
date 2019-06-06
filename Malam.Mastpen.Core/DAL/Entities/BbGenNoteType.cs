using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class NoteType
    {
        public NoteType()
        {
            Notes = new HashSet<Notes>();
        }

        public int NoteTypeId { get; set; }
        public string NoteTypeName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<Notes> Notes { get; set; }
    }
}
