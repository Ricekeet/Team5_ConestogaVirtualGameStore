using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Team5_ConestogaVirtualGameStore.Areas.Identity.Data;
using Team5_ConestogaVirtualGameStore.Data;

[assembly: HostingStartup(typeof(Team5_ConestogaVirtualGameStore.Areas.Identity.IdentityHostingStartup))]
namespace Team5_ConestogaVirtualGameStore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CVGS_IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CVGS_IdentityContextConnection")));

                //services.AddDefaultIdentity<CVGS_User>(options => options.SignIn.RequireConfirmedAccount = true)
                    //.AddEntityFrameworkStores<CVGS_IdentityContext>();

                //Add Role Manager Support
                services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<CVGS_IdentityContext>();
            });
        }
    }
}