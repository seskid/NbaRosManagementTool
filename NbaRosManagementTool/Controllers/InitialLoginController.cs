using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NbaRosManagementTool.Data;
using Microsoft.AspNetCore.Mvc;
using NbaRosManagementTool.ViewModels;
using NbaRosManagementTool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static NbaRosManagementTool.Models.ApplicationUser;
using System.Security.Claims;

namespace NbaRosManagementTool.Controllers
{
    public class InitialLoginController : Controller
    {
        private readonly NbaDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InitialLoginController(NbaDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Team> teams = context.Teams.ToList();
            InitialLoginViewModel initialLoginViewModel = new InitialLoginViewModel(teams);
            return View(initialLoginViewModel);
            
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(InitialLoginViewModel initialLoginViewModel)
        {

            if (ModelState.IsValid)
            {
                UserTeams userTeam = new UserTeams();

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = claim.Value;

                var currentUser = await _userManager.FindByIdAsync(userId);
                
                Team initTeam = context.Teams.Single(t => t.ID == initialLoginViewModel.TeamID);

                userTeam.CityName = initTeam.CityName;
                userTeam.TeamName = initTeam.TeamName;
                userTeam.TeamPayroll = initTeam.TeamPayroll;
                userTeam.Roster = initTeam.Roster;
                userTeam.CapSpace = initTeam.CapSpace;
                userTeam.User = currentUser;
                context.UserTeams.Add(userTeam);
                context.SaveChanges();


                String url = String.Format("/User/?id={0}", userTeam.User.Id);
                return Redirect(url);
            }

            return Redirect("/InitialLogin/");
        }




    }
}
