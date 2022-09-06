using Microsoft.AspNetCore.Identity;
using Vacations.Core.Models.Identity;
using Vacations.Utilities.Security;

namespace Vacations.Persistence
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<Employee> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "User";
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Admin";
                _ = roleManager.CreateAsync(role).Result;
            }
        }

        private static void SeedUsers(UserManager<Employee> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                Employee user = new Employee();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";
                user.FirstName = "Admin";
                user.LastName = "AdminAdminAdmin";
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;

                IdentityResult result = userManager.CreateAsync
                    (user, "Password.1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRoles.Administrator).Wait();
                }
            }
        }

        
    }
}
