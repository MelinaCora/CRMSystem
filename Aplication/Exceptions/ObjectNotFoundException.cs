using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public string Message;
        public ObjectNotFoundException(string message) : base(message) { 
        
            Message = message;

        }
    }
}
