using System;
using System.Collections.Generic;
using System.Text;

namespace Malam.Mastpen.Core.DAL.Dbo
{
    public class CodeEntity 
    {
        public string Name { get; set; }
    }

    public class ParameterCodeEntity : CodeEntity
    {
        public object ParameterFieldID { get; set; }
    }
}
