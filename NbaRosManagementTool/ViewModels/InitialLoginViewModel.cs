using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NbaRosManagementTool.Models;
using NbaRosManagementTool.Data;

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

        public InitialLoginViewModel() { }

        public InitialLoginViewModel(IEnumerable<Team> teams, NbaDbContext dbContext)
        {

            context = dbContext;
            TeamList = new List<SelectListItem>();

            foreach (Team teamItems in teams)
            {
                TeamList.Add(new SelectListItem
                {
                    Value = teamItems.ID.ToString(),
                    Text = teamItems.CityName + " " + teamItems.TeamName
                });

            }
            TeamID = 1;
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
