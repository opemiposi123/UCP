using Azure;
using Identity.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.Interface.Repository;
using UCP.Domain.Entity;
using UCP.Domain.Enum;

namespace UCP.Persistence.Repository
{
    public class ApplyForLoanRepository : IApplyForLoanRepository
    {
        private readonly UCPDbContext _dbContext;
        public ApplyForLoanRepository(UCPDbContext dbContex)
        {
            _dbContext = dbContex;
        }
        public List<ApplyForLoan> GetAllLoanApplicant()
        {
            return _dbContext.ApplyForLoans.ToList(); 
        } 

        public async Task<List<ApplyForLoanDto>> GetAllLoanApplicantDto()
        {
            return await _dbContext.ApplyForLoans.Select(x => new ApplyForLoanDto
            {
                
                LoanTerm = x.LoanTerm,
                LoanAmount = x.LoanAmount
            }).ToListAsync();
        }

        public async Task<ApplyForLoanDto?> LoadLoanApplicantDetail(Guid id) =>
            await _dbContext.ApplyForLoans
                            .Where(x => x.Id == id)
                            .Select(x => new ApplyForLoanDto
                            {
                                LoanTerm = x.LoanTerm,
                                LoanAmount = x.LoanAmount,
                                Purpose = x.Purpose,
                                PaymentFrequency = x.PaymentFrequency,

                            })
                            .FirstOrDefaultAsync();

        public async Task<ApplyForLoan> GetLoanApplicantById(Guid id)
        {
            return await _dbContext.ApplyForLoans.Where(s => s.Id == id).FirstOrDefaultAsync();
        }
 
    }
}
