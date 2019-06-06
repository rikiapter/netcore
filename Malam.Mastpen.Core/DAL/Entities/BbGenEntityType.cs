using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class EntityType
    {
        public EntityType()
        {
            AddressEntity = new HashSet<Address>();
            AddressEntityType = new HashSet<Address>();
            DocTypeEntity = new HashSet<DocTypeEntity>();
            Docs = new HashSet<Docs>();
            Notes = new HashSet<Notes>();
            PhoneMail = new HashSet<PhoneMail>();
        }

        public int EntityTypeId { get; set; }
        public string EntityTypeName { get; set; }
        public string TableName { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<Address> AddressEntity { get; set; }
        public ICollection<Address> AddressEntityType { get; set; }
        public ICollection<DocTypeEntity> DocTypeEntity { get; set; }
        public ICollection<Docs> Docs { get; set; }
        public ICollection<Notes> Notes { get; set; }
        public ICollection<PhoneMail> PhoneMail { get; set; }
    }
}
