using System;
using System.Collections.Generic;
using System.Text;

namespace Malam.Mastpen.HR.Core.BL.Requests
{
    public class FileRequest
    {
        public string FileByte { get; set; } 
        public string ContentType { get; set; }

    }

    public  class Training 
    {
        public int id { get; set; }
        public int? EmployeeId { get; set; }
        public int? TrainingTypeId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Comment { get; set; }
        public int UserInsert { get; set; }

        public FileRequest fileRequest { get; set; }
    }
}