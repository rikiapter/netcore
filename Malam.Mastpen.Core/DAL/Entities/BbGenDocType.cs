using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class DocType
    {
        public DocType()
        {
            DocTypeEntity = new HashSet<DocTypeEntity>();
            Docs = new HashSet<Docs>();
        }

        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public int? IsDisplayInSign { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<DocTypeEntity> DocTypeEntity { get; set; }
        public ICollection<Docs> Docs { get; set; }
    }
}
