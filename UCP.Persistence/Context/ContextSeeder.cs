
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using UCP.Domain.Entity;
using UCP.Domain.Enum;

namespace UCP.Persistence.Context
{
    public class ContextSeeder
    {
        public static async Task SeedRolesAsync(UserManager<Member> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Role.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Role.Nominal.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<Member> userManager, RoleManager<IdentityRole> roleManager)
        {
            var gender = Gender.Male;
            //Seed Default User
            var defaultUser = new Member
            {
                Id = new Guid().ToString(),
                UserName = "Mahmood",
                Surname = "MahmoodAhmadAbdulWaheed",
                Email = "abdulwaheedmahmoodahmad@gmail.com",
                PhoneNumber = "091653538299",
                AccountNumber = "9165353829",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                DateJoined = DateTime.Now,
                CreatedBy = "Admin",
                Gender = gender,
                DateOfBirth =  DateTime.Now,
                ModifiedBy = "Super Admin",
                ModifiedDate = new DateTime()
            };
         //  if (userManager.Members.All(u => u.Id != defaultUser.Id))
         //  {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin@12345#.");
                    await userManager.AddToRoleAsync(defaultUser, Role.Nominal.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Role.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Role.SuperAdmin.ToString());
                }
           //}
        }
    }
}
