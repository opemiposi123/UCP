using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.Interface.Service;
using UCP.Domain.Entity;

namespace UCP.Application.Implementation.Service
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Member> _signInManager;
        public UserAuthenticationService(UserManager<Member> userManager,
                                         SignInManager<Member> signInManager, 
                                         RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
     

        public async Task<Status> LoginAsync(LoginModelDto model)
        {
            var status = new Status();
            try
            {
                
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    status.StatusCode = 0;
                    status.Message = "Invalid username";
                    return status;
                }

                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    status.StatusCode = 0;
                    status.Message = "Invalid Password";
                    return status;
                }

                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
                if (signInResult.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    status.StatusCode = 1;
                    status.Message = "Logged in successfully";
                }
                else if (signInResult.IsLockedOut)
                {
                    status.StatusCode = 0;
                    status.Message = "User is locked out";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on logging in";
                }

                return status;
            }
            catch(Exception)
            {
                status.StatusCode = 0;
                status.Message = "error occured while processing your request";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();

        }

        public async Task<Status> ChangePasswordAsync(ChangePasswordModelDto model, string username)
        {
            var status = new Status();

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                status.Message = "User does not exist";
                status.StatusCode = 0;
                return status;
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                status.Message = "Password has updated successfully";
                status.StatusCode = 1;
            }
            else
            {
                status.Message = "Some error occcured";
                status.StatusCode = 0;
            }
            return status;

        }
    }
}
