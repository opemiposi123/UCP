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

namespace UCP.Persistence.Repository
{
    public  class LoanRepository : ILoanRepository
    {
        private readonly UCPDbContext _dbContext;
        public LoanRepository(UCPDbContext dbContex)
        {
            _dbContext = dbContex;
        }
        public List<Loan> GetAllLoan()
        {
            return _dbContext.Loans.ToList();
        }

        public async Task<List<LoanDto>> GetAllLoanDto()
        {
            return await _dbContext.Loans.Select(x => new LoanDto
            {
                LoanName = x.LoanName,
                InterestRate = x.InterestRate,
            }).ToListAsync();
        }

        public async Task<LoanDto?> LoadLoanDetailAsync(Guid id) =>
            await _dbContext.Loans
                            .Where(x => x.Id == id)
                            .Select(x => new LoanDto
                            {
                                LoanName = x.LoanName,
                                Datejoined = x.Datejoined,
                                InterestRate = x.InterestRate,
                                ModifiedDate = x.ModifiedDate
                            })
                            .FirstOrDefaultAsync();

        public Loan GetLoanById(Guid id)
        {
            return _dbContext.Loans.Where(s => s.Id == id).FirstOrDefault();
        }
        public Loan GetLoanByName(string Loaname)
        {
            return _dbContext.Loans.Where(s => s.LoanName == Loaname).FirstOrDefault();
        }
    }
}
