using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.Interface.Repository;
using UCP.Application.Interface.Service;
using UCP.Application.ResponseModel;
using UCP.Application.Shared;
using UCP.Domain.Entity;
using UCP.Domain.Enum;

namespace UCP.Application.Implementation.Service
{
    public  class LoanService : ILoanService
    {
        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILoanRepository _loanRepository;
        private readonly IRepository _repository;
        private readonly SignInManager<Member> _signInManager;
        private readonly ILogger<MemberService> _logger;

        public LoanService(UserManager<Member> userManager,
                              SignInManager<Member> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              ILoanRepository loanRepository,
                              IRepository repository,
                              ILogger<MemberService> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _loanRepository = loanRepository;
            _repository = repository;
            _signInManager = signInManager;
        }

        public async Task<CreateResponseModel> CreateLoan(LoanDto request)
        {
            try
            {
                var status = new Status();
                var loanExists = await _userManager.FindByNameAsync(request.LoanName);
                if (loanExists != null)
                {
                    return new CreateResponseModel(false,
                                                "",
                                                "Loan Already Exist");
                    _logger.LogInformation("Loan already exist");
                }
                var loan = new Loan()
                {
                    Id = new Guid(),
                    InterestRate = request.InterestRate,
                    LoanName = request.LoanName,
                    Datejoined = DateTime.Now,

                };
                var result = await _repository.CreateAsync(loan);
                _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new CreateResponseModel(false,
                                            "",
                                            "Something Went wrong..");

                _logger.LogError("Something Went wrong..");
            }

            return new CreateResponseModel(true,
                                           "",
                                           "User Creation Successsful.");
            _logger.LogInformation("Loan Successfully created");
        }

        public async Task<CreateResponseModel> DeleteLoan(Guid id)
        {

            var deleteLoan = _loanRepository.GetLoanById(id);

            if (deleteLoan == null)
            {
                return new CreateResponseModel(false,
                                             "",
                                              "No such loan exists");
            }

            try
            {
                _repository.RemoveAsyncForloan(deleteLoan);
            }
            catch (Exception)
            {
                return new CreateResponseModel(false,
                                              "",
                                              "Could not delete loan..");
            }
            return new CreateResponseModel(true,
                                              "",
                                              "Delete Successful.");
        }

        public async Task<List<LoanDto>> LoadAllLoan()
        {
            return await _loanRepository.GetAllLoanDto();
        }

        public async Task<LoanDto> LoadLoanDetail(Guid id)
        {
            return await _loanRepository.LoadLoanDetailAsync(id);
        }

        public async Task<EditRespondeModel> UpdateLoan(Guid id, LoanDto request)
        {
            try
            {

                DateTime modified = DateTime.Now;
                var std = _loanRepository.GetLoanById(id);

                if (std != null)
                {
                    std.ModifiedDate = modified;
                    std.ModifiedBy = "Admin";
                    std.InterestRate = request.InterestRate;
                    std.LoanName = request.LoanName;

                    _repository.UpdateAsyncForLoan(std);
                    _repository.SaveChangesAsync();
                }
                else
                {
                    return new EditRespondeModel(false,
                                                 "",
                                                 "Loan does not exist");
                }
            }
            catch (Exception ex)
            {
                return new EditRespondeModel(false,
                                                 "",
                                                 "Something went wrong");

                _logger.LogError("Something went wrong");
            }
            return new EditRespondeModel(true,
                                                 "",
                                                 "Loan Successfully Updated");

            _logger.LogInformation("Loan Successfully created");
        }

    }
}
