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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Loan>()
                .Property(p => p.InterestRate)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<ApplyForLoan> ApplyForLoans { get; set; }
        public DbSet<Collateral> Collaterals { get; set; }

    }
}


