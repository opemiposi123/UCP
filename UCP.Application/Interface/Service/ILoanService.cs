using UCP.Application.Dto;
using UCP.Application.ResponseModel;
using UCP.Domain.Entity;
namespace UCP.Application.Interface.Service
{
    public interface  ILoanService
    {
        Task<CreateResponseModel> CreateLoan(LoanDto request);
        Task<List<LoanDto>> LoadAllLoan();
        Task<LoanDto> LoadLoanDetail(Guid id);
        Task<EditRespondeModel> UpdateLoan(Guid id, LoanDto updateLoanDto);
        Task<CreateResponseModel> DeleteLoan(Guid id);
    }
}
