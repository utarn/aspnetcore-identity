using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore_identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace aspnetcore_identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serivces = scope.ServiceProvider;
                try
                {
                    var dbContext = serivces.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();

                    var userManager = serivces.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serivces.GetRequiredService<RoleManager<IdentityRole>>();
                    await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
                    await ApplicationDbContextSeed.SeedSampleDataAsync(dbContext);

                }
                catch (Exception ex)
                {
                    var logger = serivces.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error seeding the database");

                }
            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
