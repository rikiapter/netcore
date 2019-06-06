using System;
using System.Collections.Generic;

namespace Malam.Mastpen.Core.DAL.Entities
{
    public partial class TrainingType
    {
        public TrainingType()
        {
            EmployeeTraining = new HashSet<EmployeeTraining>();
        }

        public int TrainingTypeId { get; set; }
        public string TrainingTypeName { get; set; }
        public string Comment { get; set; }
        public int? UserInsert { get; set; }
        public DateTime? DateInsert { get; set; }
        public int? UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? State { get; set; }

        public ICollection<EmployeeTraining> EmployeeTraining { get; set; }
    }
}
