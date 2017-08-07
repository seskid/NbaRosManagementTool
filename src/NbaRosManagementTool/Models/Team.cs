using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaRosManagementTool.Models
{
    public class Team
    {
        public int ID { get; set; }

        public string CityName { get; set; }

        public string TeamName { get; set; }

        public decimal TeamPayroll { get; set; }

        public decimal CapSpace { get; set; }

        public List<Player> Roster { get; set; }

    }
}
