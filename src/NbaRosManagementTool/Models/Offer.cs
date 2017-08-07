using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaRosManagementTool.Models
{
    public class Offer
    {
        public int ID { get; set; }

        public decimal Salary { get; set; }

        //creates a forgein key relationship with the Free Agent class 
        public int FreeAgentID { get; set; }

        public string UserName { get; set; }
    }
}
