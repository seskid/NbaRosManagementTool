using NbaRosManagementTool.Data;
using NbaRosManagementTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaRosManagementTool.ViewModels
{
    

    public class UserViewModel
    {
        public UserTeams theUserTeam{ get; set; }

        public List<Player> thePlayers { get; set; }

        public List<Player> userPlayerList { get; set; }

        public List<UserTeams> userTeams { get; set; }
        
        public List<KeyValuePair<int,UserTeams>> bestTeams { get; set; }

        public string userName { get; set; }

        public UserViewModel() { }


        public UserViewModel(int id,NbaDbContext context)
        {
            thePlayers = new List<Player>();

            theUserTeam = context.UserTeams.Single(u => u.ID == id);
            theUserTeam.theRoster = context.UserPlayers.Where(p => p.UserTeamsID == id).ToList();

            //load list with current players on team 
            foreach(UserPlayers pl in theUserTeam.theRoster)
            {
                thePlayers.Add(context.Players.Single(p => p.ID== pl.PlayerID));
            }
        }
    }
}
