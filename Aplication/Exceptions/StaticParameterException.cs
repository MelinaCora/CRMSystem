﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Exceptions
{
    public class StaticParameterException : Exception
    {
        public string Message;

        public StaticParameterException(string message) : base(message)
        {
            Message = message;
        }
    }
}
