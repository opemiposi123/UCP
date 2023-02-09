using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UCP.Application.Interface.Service;
using UCP.Domain.Entity;

namespace UCP.WebUI.Controllers
{
    public class DashboardController : Controller
    {

        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDashboardCountService _dashboardCountService;

        public DashboardController(UserManager<Member> userManager, RoleManager<IdentityRole> roleManager, IDashboardCountService dashboardCountService)
        {
            _roleManager = roleManager;
            _dashboardCountService = dashboardCountService;
            _userManager = userManager;
        }

        //[HttpGet("dashboard")]
        //[Authorize(Roles = "SuperAdmin,Admin,Nominal")]
        public async Task<IActionResult> Index()
        {
            var instances =
                await _dashboardCountService.DashBoardCount();

            return View(instances);
        }
    }
}
