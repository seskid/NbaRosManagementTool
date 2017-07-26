using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NbaRosManagementTool.Models;
using NbaRosManagementTool.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NbaRosManagementTool.ViewModels
{
    public class InitialLoginViewModel
    {
        [Required]
        [Display(Name = "Teams")]
        public int TeamID { get; set; }

        private readonly NbaDbContext context;

        public List<SelectListItem> TeamList { get; set; }

        public List<Player> PlayerList { get; set; }

        public Team theTeam { get; set; }

        public List<Player> userPlayerList { get; set; }

        public UserTeams theUserTeam { get; set; }

        public InitialLoginViewModel() { }

        public InitialLoginViewModel(IEnumerable<Team> teams, NbaDbContext dbContext,int id)
        {

            context = dbContext;
            TeamList = new List<SelectListItem>();
            TeamID = id;
            theTeam = context.Teams.Single(t => t.ID == id);
            

            foreach (Team teamItems in teams)
            {
                String url = String.Format("/InitialLogin/Team/?id={0}", teamItems.ID);
                TeamList.Add(new SelectListItem
                {
                    Value = url,
                    Text = teamItems.CityName + " " + teamItems.TeamName
                });

            }
            
         
            //remove free agents from list 
            TeamList.RemoveAt(TeamList.Count() - 1);
        }

        public List<Player> getPlayers(int teamID)
        {
            PlayerList = new List<Player>();
            PlayerList = context.Players.Where(p => p.TeamID == teamID).ToList();
            return PlayerList;
        }
     }
}
