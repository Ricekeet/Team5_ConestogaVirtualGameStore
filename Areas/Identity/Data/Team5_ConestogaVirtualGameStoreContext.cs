using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Team5_ConestogaVirtualGameStore.Data
{
    public class Team5_ConestogaVirtualGameStoreContext : IdentityDbContext<IdentityUser>
    {
        public Team5_ConestogaVirtualGameStoreContext(DbContextOptions<Team5_ConestogaVirtualGameStoreContext> options)
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
