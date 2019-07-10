using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Docs:IAuditableEntity
    {
        public int DocumentId { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        public string DocumentPath { get; set; }
        public DateTime? DocumentDate { get; set; }
        public bool? IsDocumentSigned { get; set; }
        public DateTime? ValIdDate { get; set; }
        public int? LanguageId { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public DocType DocumentType { get; set; }
        public EntityType EntityType { get; set; }
        public Language Language { get; set; }
    }
}
