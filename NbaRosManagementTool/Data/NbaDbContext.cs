using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NbaRosManagementTool.Models;
using System.Reflection.Emit;
using static NbaRosManagementTool.Models.ApplicationUser;

namespace NbaRosManagementTool.Data
{
    public class NbaDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<UserTeams> UserTeams { get; set; }

       

        public NbaDbContext(DbContextOptions<NbaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

          


        }
    }
}
