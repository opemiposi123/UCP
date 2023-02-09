using Identity.Models.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Domain.Entity;

namespace UCP.Persistence.Context
{
    public static class DbInitializerExtension
    {
        public static async Task<IApplicationBuilder> UseItToSeedSqlServer(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<UCPDbContext>();
                var userManager = services.GetRequiredService<UserManager<Member>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await ContextSeeder.SeedRolesAsync(userManager, roleManager);
                await ContextSeeder.SeedSuperAdminAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {

            }

            return app;
        }
    }
}
