using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.Interface.Repository;
using UCP.Application.Interface.Service;
using UCP.Domain.Entity;
using UCP.Domain.Enum;

namespace UCP.Application.Implementation.Service
{
    public class ApplyForLoanService : IApplyForLoanService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IRepository _repository;
        private readonly ILogger<ApplyForLoanService> _logger;
        private readonly IApplyForLoanRepository _applyForLoanRepository;

        public ApplyForLoanService(IMemberRepository memberRepository,
                                    ILoanRepository loanRepository,
                                    IRepository repository,
                                    ILogger<ApplyForLoanService> logger,
                                    IApplyForLoanRepository applyForLoanRepository)
        {
            _memberRepository = memberRepository;
            _loanRepository = loanRepository;
            _repository = repository;
            _logger = logger;
            _applyForLoanRepository = applyForLoanRepository;
        }

      

        public Status ApplyForloan(ApplyForLoanDto request)
        {
            var status = new Status();
            var loan = new Loan();
            try
            {
               
                var applyForLoan = new ApplyForLoan()
                {
                    Id = new Guid(),
                    LoanAmount = request.LoanAmount,
                    PaymentFrequency = request.PaymentFrequency,
                    LoanTerm = request.LoanTerm,
                    Purpose = request.Purpose,
                };

                var result = _repository.ApplyAsync(applyForLoan);

                var getAllLoans = _loanRepository.GetLoanById(applyForLoan.LoanId);
                var loanProfit = getAllLoans.InterestRate * applyForLoan.LoanAmount;
                var monthOrWeek = (int)applyForLoan.PaymentFrequency;
                if (monthOrWeek == 1)
                {
                    if (applyForLoan.LoanTerm <= 5)
                    {
                        status.StatusCode = 0;
                        status.Message = "loan Term cannot pass five(5) months";
                        _logger.LogInformation("loan Term cannot pass five(5) months");
                        return status;
                    }
                    monthOrWeek = 30;
                    var loanScheduleForMonth = applyForLoan.LoanTerm * monthOrWeek;
                    var a = loanScheduleForMonth / loanProfit;
                    _repository.SaveChangesAsync();
                    status.money = a;
                    status.StatusCode = 1;
                    status.Message = "Your payment frequency is monthly";
                    return status;
                }
                if (monthOrWeek == 2)
                {
                    if (applyForLoan.LoanTerm <= 21)
                    {
                        status.StatusCode = 1;
                        status.Message = "loan Term cannot pass five(5) months";
                        _logger.LogInformation("loan Term cannot pass five(5) months");
                    }
                    monthOrWeek = 7;
                    var loanScheduleForWeek = applyForLoan.LoanTerm * monthOrWeek;
                    var a = loanScheduleForWeek / loanProfit;
                    _repository.SaveChangesAsync();
                    status.money = a;
                    status.StatusCode = 1;
                    status.Message = "Your payment frequency is weekly";
                    return status;
                }
            }
            catch(Exception)
            {
                status.StatusCode = 0;
                status.Message = "Cannot Apply for loan";
            }
                    status.StatusCode = 1;
                    status.Message = "Loan Applied successfuly";
                    return status;
        }
        public async Task<List<ApplyForLoanDto>> GetAllLoanApplicant()
        {
            return await _applyForLoanRepository.GetAllLoanApplicantDto();
        }

        public async Task<ApplyForLoanDto> LoadLoanApplicantDetail(Guid id)
        {
            return await _applyForLoanRepository.LoadLoanApplicantDetail(id);
        }
    }
}
