using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UCP.Application.Dto;
using UCP.Application.Interface.Service;
using UCP.Domain.Entity;

namespace UCP.WebUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }
     

        [HttpGet("loan")] 
        public async Task<IActionResult> ViewLoan()
        {
            var instances =
            await _loanService.LoadAllLoan();
            return View(instances);
        }

        [HttpGet("loan/create")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult CreateLoan() =>
         View();

        [HttpPost("loan/create")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> CreateLoan([FromForm] LoanDto loan)
        {
            try
            {
                var response = await _loanService.CreateLoan(loan);
                return RedirectToAction("ViewLoan", "Loan");
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

        [HttpGet("loan/data")]
        public async Task<IActionResult> LoadLoanAsync()
        {
            var instances =
                await _loanService.LoadAllLoan();
            return View(instances);
        }

        [HttpGet("loan/{id}")]
        public async Task<IActionResult> ViewLoanDetail([FromRoute] Guid id)
        {
            var instance = await _loanService.LoadLoanDetail(id);

            return instance == null
                       ? (IActionResult)NotFound()
                       : View(instance);
        }

        [HttpGet("loan/{id}/edit")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> EditLoanAsync(Guid id)
        {
            var instance = await _loanService.LoadLoanDetail(id);

            if (instance == null)
            {
                return NotFound();
            }

            return View(instance);
        }

        [HttpPost("loan/{id}/edit")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> EditLoanAsync([FromRoute] Guid id, [FromForm] LoanDto loan)
        {
            try
            {
                var response = await _loanService.UpdateLoan(id, loan);

                if (response.Status)
                {
                    return Ok(
                        new
                        {
                            status = "success",
                            response.Message,
                            redirectUri = Url.Action("ViewLoans",
                                                     new
                                                     {
                                                         id = response.Id
                                                     })
                        });
                }

                return Ok(new
                {
                    status = "error",
                    response.Message
                });
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

        [HttpPost("loan/{id}/delete")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteLoanAsync([FromRoute] Guid id)
        {
            try
            {
                var response = await _loanService.DeleteLoan(id);
                if (response.Status)
                {
                    return Ok(
                        new
                        {
                            status = "success",
                            response.Message,
                            redirectUri = Url.Action("ViewLoans")
                        });
                }

                return Ok(new
                {
                    status = "error",
                    response.Message
                });
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
