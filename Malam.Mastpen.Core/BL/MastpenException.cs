
using System;

namespace Malam.Mastpen.Core.BL
{
    public class MastpenException : Exception
    {
        public MastpenException()
            : base()
        {
        }

        public MastpenException(string message)
            : base(message)
        {
        }
    }
}
