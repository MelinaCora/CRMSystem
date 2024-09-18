using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Exceptions
{
    public class InvalidOffsetException : Exception
    {
        public string Message;

        public InvalidOffsetException(string message) : base(message)
        {
            Message = message;
        }
    }
}
