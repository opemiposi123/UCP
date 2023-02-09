using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UCP.Domain.Entity;

namespace Identity.Models.Domain
{
    public class UCPDbContext : IdentityDbContext<Member>
    {
        public UCPDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<ApplyForLoan> ApplyForLoans { get; set; }

    }
}


