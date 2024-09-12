using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Exceptions
{
    public class ProjectNameAlredyExistException : Exception
    {
        public string Message;
        public ProjectNameAlredyExistException(string message) : base(message)
        {
            Message = message;
        }
    }
}
