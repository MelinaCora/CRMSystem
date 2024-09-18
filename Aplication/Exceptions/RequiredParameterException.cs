using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Exceptions
{
    public class RequiredParameterException : Exception
    {
        public string Message;

        public RequiredParameterException(string message) : base(message)
        {
            Message = message;
        }
    }
}
