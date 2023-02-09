using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Domain.Entity;

namespace UCP.Application.Interface.Repository
{
    public  interface ILoanRepository
    {
        List<Loan> GetAllLoan();
        Loan GetLoanById(Guid id);
        Loan GetLoanByName(string name);
        Task<List<LoanDto>> GetAllLoanDto();
        Task<LoanDto?> LoadLoanDetailAsync(Guid id);    
    }
}
