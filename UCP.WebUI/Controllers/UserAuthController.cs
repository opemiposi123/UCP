using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCP.Application.Dto;
using UCP.Application.Interface.Service;
using UCP.Domain.Entity;
using UCP.Domain.Enum;

namespace UCP.WebUI.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        //private readonly ContextSeeder _contextSeeder;
        
        public UserAuthController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginModelDto model)
        {
            //if (!ModelState.IsValid)
            //    return View(model);
            var result = await _userAuthenticationService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModelDto changePasswordModelDto, string username)
        {

            var result = await _userAuthenticationService.ChangePasswordAsync(changePasswordModelDto, username);
            return RedirectToAction(nameof(Login));

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userAuthenticationService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
