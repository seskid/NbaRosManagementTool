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
using System.Security.Claims;

namespace NbaRosManagementTool.Controllers
{
    public class UserController : Controller
    {
        private readonly NbaDbContext context;

        protected UserManager<ApplicationUser> UserManager { get; set; }

        public UserController(NbaDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            context = dbContext;
            UserManager = userManager;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var theUserID = claim.Value;

            UserTeams userTeam = new UserTeams();
            userTeam = context.UserTeams.Single(u => u.User.Id == theUserID);

            String url = String.Format("/User/Team?id={0}", userTeam.ID);
            return Redirect(url);
           
        }




        public IActionResult Team(int id)
        {
            UserViewModel userViewModel = new UserViewModel();

            userViewModel.theUserTeam = context.UserTeams.Single(t => t.ID == id);
            userViewModel.userPlayerList = new List<Player>();

            //get players that belong to user
            List<UserPlayers> players = context.UserPlayers.Where(p => p.UserTeamsID == userViewModel.theUserTeam.ID).ToList();

            //load players in list
            foreach (UserPlayers p in players)
            {
                userViewModel.userPlayerList.Add(context.Players.Single(pl => pl.ID == p.PlayerID));
            }

            return View(userViewModel);
        }

        public IActionResult Best()
        {
            
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.bestTeams = new List<KeyValuePair<int,UserTeams>>();
            List<UserPlayers> userPlayers = new List<UserPlayers>();
            Player player = new Player();
            int totalRatings = 0;
            int rating = 0;
          
            IQueryable<ApplicationUser> thelist=UserManager.Users;
            foreach(ApplicationUser u in thelist)
            {
                userViewModel.theUserTeam = new UserTeams();
                userViewModel.theUserTeam = context.UserTeams.SingleOrDefault(t => t.User == u);
                if (userViewModel.theUserTeam != null)
                {
                    userPlayers = context.UserPlayers.Where(p => p.UserTeamsID == userViewModel.theUserTeam.ID).ToList();
                    foreach (UserPlayers p in userPlayers)
                    {
                       player= context.Players.Single(pl => pl.ID == p.PlayerID);
                       totalRatings = totalRatings + player.PlayerRating;
                    }

                    rating = totalRatings / userPlayers.Count;
                    var team = new KeyValuePair<int,UserTeams>(rating,userViewModel.theUserTeam);
                    userViewModel.bestTeams.Add(team);
                }

              
                totalRatings = 0;
                rating = 0;
            }

            userViewModel.bestTeams.Reverse();
            return View(userViewModel);
        }

        /*public IActionResult FreeAgent()
        {
            return View();
        }*/

    }
}
