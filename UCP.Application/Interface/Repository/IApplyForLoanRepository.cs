using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Domain.Entity;

namespace UCP.Application.Interface.Repository
{
    public interface IApplyForLoanRepository
    {
        List<ApplyForLoan> GetAllLoanApplicant();
        Task<ApplyForLoan> GetLoanApplicantById(Guid id);
        Task<List<ApplyForLoanDto>> GetAllLoanApplicantDto();
        Task<ApplyForLoanDto?> LoadLoanApplicantDetail(Guid id);
    }
}
