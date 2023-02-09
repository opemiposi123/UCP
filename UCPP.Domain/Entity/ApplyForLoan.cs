using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Domain.Enum;

namespace UCP.Domain.Entity
{
    public class ApplyForLoan : BaseEntity
    {
        public decimal LoanAmount { get; set; }
        public decimal LoanAmountPlusInterestRate { get; set; }
        public string LoanName { get; set; }
        public int LoanTerm { get; set; }
        public string Email { get; set; }
        public DateTime? LoanStartDate { get; set; }
        public DateTime? LoanEndDate { get; set; } 
        public PaymentFrequency PaymentFrequency { get; set; }
        public string? Purpose { get; set; }
        public string LenderName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Collateral Collateral { get; set; }
    }
}
