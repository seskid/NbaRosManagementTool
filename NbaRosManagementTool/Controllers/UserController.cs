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

        public IActionResult Index(int id)
        {
           return View();

        }


    }
}
