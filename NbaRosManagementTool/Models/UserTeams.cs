using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaRosManagementTool.Models
{
    public class UserTeams
    {
        public int ID { get; set; }

        public string CityName { get; set; }

        public string TeamName { get; set; }

        public decimal TeamPayroll { get; set; }

        public decimal CapSpace { get; set; }

        public List<UserPlayers> theRoster { get; set; } = new List<UserPlayers>();

        public virtual ApplicationUser User { get; set; }

        public string UserName { get; set; }

        public UserTeams() { }

        public UserTeams(string cityname,string teamname)
        {
            CityName = cityname;
            TeamName = teamname;
            CapSpace = 105000000;
            TeamPayroll = 0;
        }
    }
}
