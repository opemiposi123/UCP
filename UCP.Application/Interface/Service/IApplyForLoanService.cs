using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.ResponseModel;
using UCP.Domain.Entity;

namespace UCP.Application.Interface.Service
{
    public interface IApplyForLoanService
    {
        Status ApplyForloan(ApplyForLoanDto request);
        Task<List<ApplyForLoanDto>> GetAllLoanApplicant(); 
        Task<ApplyForLoanDto> LoadLoanApplicantDetail(Guid id);
    }
}
