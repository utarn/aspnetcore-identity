using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace aspnetcore_identity.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
                                                      RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (!await roleManager.RoleExistsAsync("user"))
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                FirstName = "Admin",
                LastName = "Default",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "1234");
            }
            defaultUser = await userManager.FindByEmailAsync("admin@localhost");
            if (!await userManager.IsInRoleAsync(defaultUser, "admin"))
            {
                await userManager.AddToRoleAsync(defaultUser, "admin");
            }
        }
        
        public static Task SeedSampleDataAsync(ApplicationDbContext dbContext) {
            return Task.CompletedTask;
        }
    }
}