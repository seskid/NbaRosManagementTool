using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NbaRosManagementTool.Models;

namespace NbaRosManagementTool.ViewModels
{
    public class InitialLoginViewModel
    {
        [Required]
        [Display(Name = "Teams")]
        public int TeamID { get; set; }

        public List<SelectListItem> TeamList { get; set; }

        public InitialLoginViewModel() { }

        public InitialLoginViewModel(IEnumerable<Team> teams)
        {



            // <option value="0">Hard</option>
            TeamList = new List<SelectListItem>();

            foreach (Team teamItems in teams)
            {
                TeamList.Add(new SelectListItem
                    {
                        Value = teamItems.ID.ToString(),
                        Text = teamItems.CityName + " " + teamItems.TeamName
                    });
                
            }

            //TeamList.RemoveAt(TeamList.Count() - 1);


        }
    }
}
