using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCP.Application.Dto;
using UCP.Application.Interface.Service;
using UCP.Domain.Entity;

namespace UCP.WebUI.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("member")]
        public async Task<IActionResult> ViewMembers()
        {
            var instances =
            await _memberService.LoadAllMember();
            return View(instances);
        }

        [HttpGet("member/create")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult CreateMember() =>
         View();

        [HttpPost("member/create")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> CreateMember([FromForm] MemberDto member)
        {
            try
            {
                var response = await _memberService.CreateMember(member);
                TempData["success"] = "member created successfully";

                return RedirectToAction("ViewMembers", "Member");
            }
            catch
            {
                return Ok(new
                {
                    status = "error",
                    message = "Something happened. Please try again later."
                });
            }

        }

        [HttpGet("member/data")]
        public async Task<IActionResult> LoadMemberAsync()
        {
            var instances =
                await _memberService.LoadAllMember();
            return View(instances);
        }

        [HttpGet("member/{id}")]
        public async Task<IActionResult> ViewMemberDetail(Guid id)
        {
            var instance = await _memberService.LoadMemberDetail(id);

            return instance == null
                       ? (IActionResult)NotFound()
                       : View(instance);
        }

        [HttpGet("member/{id}/edit")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> EditMemberAsync(Guid id)
        {
            var instance = await _memberService.LoadMemberDetail(id);

            if (instance == null)
            {
                return NotFound();
            }

            return View(instance);
        }

        [HttpPost("member/{id}/edit")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> EditMemberAsync([FromRoute]Guid id, [FromForm] MemberDto member)
        {
            try
            {
                var response = await _memberService.UpdateMember(id, member);
                    TempData["success"] = "member updated successfully";
                return RedirectToAction("ViewMembers", "Member");

            }
            catch
            {
                return Ok(new
                {
                    status = "error",
                    message = "Something happened. Please try again later."
                });
            }
        }

        [HttpPost("member/{id}/delete")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteMemberAsync([FromRoute]Guid id)
        {
            try
            {
                var response = await _memberService.DeleteMember(id);
                    TempData["success"] = "member deleted successfully";
                return RedirectToAction("ViewMembers", "Member");
            }
            catch
            {
                return Ok(new
                {
                    status = "error",
                    message = "Something happened. Please try again later."
                });
            }
        }

    }
}
