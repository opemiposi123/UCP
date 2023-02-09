using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.Interface.Repository;
using UCP.Application.Interface.Service;

namespace UCP.Application.Implementation.Service
{
    public class DashboardCountService : IDashboardCountService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IApplyForLoanRepository _applyForLoanRepository;

        public DashboardCountService(IMemberRepository memberRepository,
                                    ILoanRepository loanRepository,
                                    IApplyForLoanRepository applyForLoanRepository)
        {
            _memberRepository = memberRepository;
            _loanRepository = loanRepository;
            _applyForLoanRepository = applyForLoanRepository;
        }
        public async Task<CountDto> DashBoardCount()
        {
            var memberCount =  _memberRepository.GetAllMember();
            var loanCount =  _loanRepository.GetAllLoan();
            var applyForLoanCount = _applyForLoanRepository.GetAllLoanApplicant();
            var data = new CountDto();

            data.MembersCounts = memberCount.Count();
            data.LoansCount = loanCount.Count();
            data.ApplyForLoansCount = applyForLoanCount.Count();
            return data;
        }

    }
}
