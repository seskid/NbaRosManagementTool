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
        public UserTeams userTeam{ get; set; }

        public List<Player> thePlayers { get; set; }

        public UserViewModel() { }


        public UserViewModel(int id,NbaDbContext context)
        {
            thePlayers = new List<Player>();

            userTeam = context.UserTeams.Single(u => u.ID == id);
            userTeam.theRoster = context.UserPlayers.Where(p => p.UserTeamsID == id).ToList();

            //load list with current players on team 
            foreach(UserPlayers pl in userTeam.theRoster)
            {
                thePlayers.Add(context.Players.Single(p => p.ID== pl.PlayerID));
            }


        }





    }
}
