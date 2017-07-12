using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaRosManagementTool.Models
{
    public class UserPlayers
    {
        public int UserTeamsID { get; set; }
        public UserTeams UserTeam { get; set; }


        public int PlayerID { get; set; }
        public Player Player { get; set; }
    }
}
