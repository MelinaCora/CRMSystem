using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Exceptions
{
    public class InvalidSizeException : Exception
    {
        public string Message;

        public InvalidSizeException(string message) : base(message)
        {
            Message = message;
        }
    }
}
