
using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class HealthDeclaration : IAuditableEntity
    {
        public int DeclarationID { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        public int? SiteId { get; set; }
        public DateTime? Date { get; set; }
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }
    }
}