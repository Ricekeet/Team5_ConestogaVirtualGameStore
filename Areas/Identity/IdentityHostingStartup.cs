using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Team5_ConestogaVirtualGameStore.Data;

[assembly: HostingStartup(typeof(Team5_ConestogaVirtualGameStore.Areas.Identity.IdentityHostingStartup))]
namespace Team5_ConestogaVirtualGameStore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Team5_ConestogaVirtualGameStoreContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Team5_ConestogaVirtualGameStoreContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Team5_ConestogaVirtualGameStoreContext>();
            });
        }
    }
}