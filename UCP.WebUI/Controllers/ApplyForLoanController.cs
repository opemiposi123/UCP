using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCP.Application.Dto;
using UCP.Application.Interface.Service;
using UCP.Domain.Entity;

namespace UCP.WebUI.Controllers
{
    public class ApplyForLoanController : Controller
    {
        private readonly IApplyForLoanService _applyForLoanService;
        private readonly ILoanService _loanService;

        public ApplyForLoanController(IApplyForLoanService applyForLoanService,
                                       ILoanService loanService)
        {
           _applyForLoanService = applyForLoanService;
            _loanService = loanService;
        }
        
        [HttpGet("applyforloan")]
        public async Task<IActionResult> ApplyForLoan()
        {
            var loanList = await _loanService.LoadAllLoan();
            ViewBag.loanList = loanList;
            return View();
        }

        [HttpPost("applyforloan/apply")]
        public IActionResult ApplyForLoan([FromForm] ApplyForLoanDto applyForLoan)
        {
            try
            {
                var response = _applyForLoanService.ApplyForloan(applyForLoan);
                return Ok("Loan Under Process");
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

        [HttpGet("applyforloan/loanApplicants")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> ViewLoanApplicants()
        {
            var instances =
            await _applyForLoanService.GetAllLoanApplicant();
            return View(instances);
        } 

        [HttpGet("applyforloan/data")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> LoadLoanApplicantsAsync()
        {
            var instances =
                await _applyForLoanService.GetAllLoanApplicant();
            return View(instances);
        }

        [HttpGet("applyforloan/{id}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> ViewLoanApplicantDetail([FromRoute] Guid id)
        {
            var instance = await _applyForLoanService.LoadLoanApplicantDetail(id);

            return instance == null
                       ? (IActionResult)NotFound()
                       : View(instance);
        }
    }
}
