using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vacations.Core.Models.Identity;
using Vacations.Persistence;

namespace Vacations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    Seed.SeedData(context);

                    var serviceProvider = scope.ServiceProvider;
                    var userManager = serviceProvider.GetRequiredService<UserManager<Employee>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                    
                    // Seed initial users and roles
                    IdentityDataInitializer.SeedData(userManager, roleManager);

                    // Apply any pending migrations. Will create database if it does not already exists.
                    context.Database.Migrate();
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured during migration");
                }
            }

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
