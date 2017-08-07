﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaRosManagementTool.Models
{
    public class FreeAgent
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PlayerRating { get; set; }

        //creates a forgein key relationship with the Team class 
        public int TeamID { get; set; }
    }
}
