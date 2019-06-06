using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class PhoneMail
    {
        public int ContactId { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        public int? PhoneTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public EntityType EntityType { get; set; }
        public PhoneType PhoneType { get; set; }
    }
}
