using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Aplication.Responses
{
    public class CreateClientResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public string phone { get; set; }
        public string address { get; set; }       

    }
}
