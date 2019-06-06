using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class PhoneType
    {
        public PhoneType()
        {
            PhoneMail = new HashSet<PhoneMail>();
        }

        public int PhoneTypeId { get; set; }
        public string PhoneTypeName { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<PhoneMail> PhoneMail { get; set; }
    }
}
