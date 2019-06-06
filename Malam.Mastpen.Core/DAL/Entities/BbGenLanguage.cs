using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class Language
    {
        public Language()
        {
            Docs = new HashSet<Docs>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<Docs> Docs { get; set; }
    }
}
