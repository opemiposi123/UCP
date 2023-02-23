using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Domain.Enum;

namespace UCP.Domain.Entity
{
    public class ApplyForLoan : BaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmountPlusInterestRate { get; set; }
        public int LoanTerm { get; set;} 
        public PaymentFrequency PaymentFrequency { get; set; }
        public string? Purpose { get; set; }
        public Guid LoanId { get; set; }
        public Loan Loan { get; set; }
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }
    }
}
