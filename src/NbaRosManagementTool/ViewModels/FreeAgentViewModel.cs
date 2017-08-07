using Microsoft.AspNetCore.Mvc.Rendering;
using NbaRosManagementTool.Data;
using NbaRosManagementTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NbaRosManagementTool.ViewModels
{
    public class FreeAgentViewModel
    {
        [Required]
        [Display(Name = "Select Free Agent")]
        public int FreeAgentID { get; set; }

        [Required]
        [Display(Name = "Salary Offer")]
        public decimal SalaryOffer { get; set; }

        private readonly NbaDbContext context;

        public List<SelectListItem> FreeAgentList { get; set; }

        public FreeAgentViewModel(IEnumerable<FreeAgent> freeAgents, NbaDbContext dbContext)
        {

            context = dbContext;
            FreeAgentList = new List<SelectListItem>();
           
            foreach (FreeAgent freeAgentItem in freeAgents)
            {
               
                FreeAgentList.Add(new SelectListItem
                {
                    Value = freeAgentItem.ID.ToString(),
                    Text = freeAgentItem.FirstName + " " + freeAgentItem.LastName
                });

            }
         }


    }
}
