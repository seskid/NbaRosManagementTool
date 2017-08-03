﻿using System;
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
using NbaRosManagementTool.Services;

namespace NbaRosManagementTool.Controllers
{
    public class UserController : Controller
    {
        private readonly NbaDbContext context;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(NbaDbContext dbContext, UserManager<ApplicationUser> UserManager)
        {
            context = dbContext;
            _userManager = UserManager;
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

        // GET: /User/Best/Index
        public IActionResult Best()
        {

            UserViewModel userViewModel = new UserViewModel();
            userViewModel.bestTeams = new List<KeyValuePair<int, UserTeams>>();
            List<UserPlayers> userPlayers = new List<UserPlayers>();
            Player player = new Player();
            int totalRatings = 0;
            int rating = 0;

            IQueryable<ApplicationUser> thelist = _userManager.Users;
            foreach (ApplicationUser u in thelist)
            {
                userViewModel.theUserTeam = new UserTeams();
                userViewModel.theUserTeam = context.UserTeams.SingleOrDefault(t => t.User == u);
                if (userViewModel.theUserTeam != null)
                {
                    userPlayers = context.UserPlayers.Where(p => p.UserTeamsID == userViewModel.theUserTeam.ID).ToList();
                    if ((userPlayers.Count > 12) && (userViewModel.theUserTeam.TeamPayroll <= 105000000))
                    {
                        foreach (UserPlayers p in userPlayers)
                        {
                            player = context.Players.Single(pl => pl.ID == p.PlayerID);
                            totalRatings = totalRatings + player.PlayerRating;
                        }

                        
                        rating = totalRatings / userPlayers.Count;
                        var team = new KeyValuePair<int, UserTeams>(rating, userViewModel.theUserTeam);
                        userViewModel.bestTeams.Add(team);
                    }
                }

                totalRatings = 0;
                rating = 0;
            }
            return View(userViewModel);
        }


        public IActionResult FreeAgent()
        {
            List<FreeAgent> freeAgents = context.FreeAgent.ToList();
            FreeAgentViewModel freeAgentViewModel = new FreeAgentViewModel(freeAgents,context);
          
            return View(freeAgentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MakeOffer(string freeAgentID,decimal SalaryOffer)
        {
            // Task SendEmailAsync(string subject, string message,string email,string password)
            EmailSender emailSender = new EmailSender();
            FreeAgent freeAgent = context.FreeAgent.Single(p => p.ID == Convert.ToInt32(freeAgentID));
            var currentUser = await GetCurrentUserAsync();
            var admin = _userManager.Users.Single(u => u.UserName == "admin");
            
            string subject = "NBA Free Agent Offer";
            string message = String.Format("{0} has made an offer of {1} to {2}",currentUser.UserName,SalaryOffer,freeAgent.FirstName+" "+freeAgent.LastName);
            
            await emailSender.SendEmailAsync(subject,message,admin.Email);
            return View();
        }
        #region Helpers

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion
    }
}
