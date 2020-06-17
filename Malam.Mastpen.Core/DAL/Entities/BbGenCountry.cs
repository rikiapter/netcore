using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Country
    {
        public Country()
        {
            //Employee = new HashSet<Employee>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int? LanguageId { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        //public ICollection<Employee> Employee { get; set; }
    }
}
