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
                InitialLoginViewModel initialLoginViewModel = new InitialLoginViewModel();
                return View(initialLoginViewModel);

            }
            String url = String.Format("/InitialLogin/Team/?id=1");
            return Redirect(url);
           
            
        }

        [HttpPost]
        public async Task<IActionResult> Team(string cityName, string teamName,int id)
        {
            UserTeams userTeam = new UserTeams(cityName, teamName);
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var theUserID = claim.Value;
            var currentUser = await _userManager.FindByIdAsync(theUserID);

            //initial user setup 
            userTeam.User = currentUser;
            context.UserTeams.Add(userTeam);
            context.SaveChanges();
            List<Team> teams = context.Teams.ToList();
            InitialLoginViewModel initialLoginViewModel = new InitialLoginViewModel(teams, context,id);
            initialLoginViewModel.theUserTeam = userTeam;
            return View(initialLoginViewModel);


        }

        public IActionResult Team(int id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var theUserID = claim.Value;
         
            
            List<Team> teams = context.Teams.ToList();
            InitialLoginViewModel initialLoginViewModel = new InitialLoginViewModel(teams, context, id);
            initialLoginViewModel.theUserTeam = new UserTeams();
            initialLoginViewModel.userPlayerList = new List<Player>();

            //get players that belong to user
            initialLoginViewModel.theUserTeam = context.UserTeams.Single(u => u.User.Id == theUserID);
            List<UserPlayers> players = context.UserPlayers.Where(p => p.UserTeamsID == initialLoginViewModel.theUserTeam.ID).ToList();

            //load players in list
            foreach(UserPlayers p in players)
            {
                initialLoginViewModel.userPlayerList.Add(context.Players.Single(pl => pl.ID == p.PlayerID));
            }

            return View(initialLoginViewModel);
        }

        [HttpPost]
        public IActionResult Add(int[] players,int userTeamID,InitialLoginViewModel initialLoginViewModel)
        {
            int[] playersSelected = players;
            initialLoginViewModel.theUserTeam = new UserTeams();
            initialLoginViewModel.theUserTeam = context.UserTeams.Single(u => u.ID == userTeamID);

           
            initialLoginViewModel.userPlayerList = new List<Player>();

            foreach(int p in playersSelected)
            {
                UserPlayers userPlayers = new UserPlayers();
                userPlayers.PlayerID = p;
                userPlayers.UserTeamsID = initialLoginViewModel.theUserTeam.ID;
                context.UserPlayers.Add(userPlayers);
                initialLoginViewModel.userPlayerList.Add(context.Players.Single(pl => pl.ID == p));
             }

            context.SaveChanges();
            return Redirect("/InitialLogin/Team/?id=1");
        }

        [HttpPost]
        public IActionResult Remove(int[] players,string action,int userTeamID,InitialLoginViewModel initialLoginViewModel)
        {
            int[] playersSelected = players;

            if (action == "release")
            {
                foreach (int p in playersSelected)
                {
                    UserPlayers userPlayers = context.UserPlayers.Single(pl => pl.PlayerID == p);
                    context.UserPlayers.Remove(userPlayers);
                }
            }
            else
            {
                String url = String.Format("/User/Team?id={0}", userTeamID);
                return Redirect(url);
               
            }

            context.SaveChanges();
            return Redirect("/InitialLogin/Team/?id=1");
        }

















    }
}
