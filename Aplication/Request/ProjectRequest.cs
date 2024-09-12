﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Request
{
    public class ProjectRequest
    {
        public Guid ProjectID { get; set; }
        public string ProjectReqName { get; set; }
        public DateTime reqStartDate { get; set; }
        public DateTime reqEndDate { get; set; }
        public int reqCampaignID { get; set; }
        public int ClientID { get; set; }  

    }

}