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

        public UserController(NbaDbContext dbContext)
        {
            context = dbContext;
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
            InitialLoginViewModel initialLoginViewModel = new InitialLoginViewModel();

            initialLoginViewModel.theUserTeam = context.UserTeams.Single(t => t.ID == id);
            initialLoginViewModel.userPlayerList = new List<Player>();

            //get players that belong to user
            List<UserPlayers> players = context.UserPlayers.Where(p => p.UserTeamsID == initialLoginViewModel.theUserTeam.ID).ToList();

            //load players in list
            foreach (UserPlayers p in players)
            {
                initialLoginViewModel.userPlayerList.Add(context.Players.Single(pl => pl.ID == p.PlayerID));
            }

            return View(initialLoginViewModel);
        }





    }
}
