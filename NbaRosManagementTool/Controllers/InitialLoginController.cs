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

        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(currentUser);

            UserTeams userTeam = new UserTeams();
            //route user to their team if they have one 
            try
            {
                userTeam = context.UserTeams.Single(t => t.User == user);
            }
            catch (InvalidOperationException e)
            {
                //build list of teams to select from 
                List<Team> teams = context.Teams.ToList();
                InitialLoginViewModel initialLoginViewModel = new InitialLoginViewModel(teams);
                return View(initialLoginViewModel);

            }
            String url = String.Format("/User/?id={0}", userTeam.ID);
            return Redirect(url);
           
            
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(InitialLoginViewModel initialLoginViewModel)
        {

            if (ModelState.IsValid)
            {
                UserTeams userTeam = new UserTeams();
               

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var theUserID = claim.Value;
                var currentUser = await _userManager.FindByIdAsync(theUserID);

                //get team user selected and create list of players team has. 
                Team initTeam = context.Teams.Single(t => t.ID == initialLoginViewModel.TeamID);
                List<Player> thePlayers = context.Players.Where(p => p.TeamID == initTeam.ID).ToList();

                foreach(Player p in thePlayers)
                {
                    UserPlayers userPlayers = new UserPlayers();
                    userPlayers.PlayerID = p.ID;
                    userPlayers.UserTeamsID = userTeam.ID;
                    userTeam.theRoster.Add(userPlayers);
                    context.UserPlayers.Add(userPlayers);
                }

                //context.SaveChanges();

                userTeam.CityName = initTeam.CityName;
                userTeam.TeamName = initTeam.TeamName;
                userTeam.TeamPayroll = initTeam.TeamPayroll;
                userTeam.CapSpace = initTeam.CapSpace;
                userTeam.User = currentUser;
                context.UserTeams.Add(userTeam);
                context.SaveChanges();


                String url = String.Format("/User/?id={0}", userTeam.ID);
                return Redirect(url);
            }

            return Redirect("/InitialLogin/");
        }




    }
}
