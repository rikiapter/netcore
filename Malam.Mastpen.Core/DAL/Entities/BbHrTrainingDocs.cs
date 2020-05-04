
using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class TrainingDocs:IAuditableEntity
    {
        public int TrainingDocId { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? OrganizationId { get; set; }
        public int? SiteId { get; set; }
        public int? LanguageId { get; set; }
        public string DocumentPath { get; set; }
        public DateTime? DocumentDate { get; set; }
        public bool? IsDocumentSigned { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public DocType DocumentType { get; set; }
        public Organization Organization { get; set; }
        public Language Language { get; set; }
    }
}
