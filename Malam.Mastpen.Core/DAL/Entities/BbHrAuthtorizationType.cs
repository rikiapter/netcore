using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class AuthtorizationType
    {
        public AuthtorizationType()
        {
            EmployeeAuthtorization = new HashSet<EmployeeAuthtorization>();
        }

        public int AuthorizationTypeId { get; set; }
        public string AuthorizationTypeName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<EmployeeAuthtorization> EmployeeAuthtorization { get; set; }
    }
}
