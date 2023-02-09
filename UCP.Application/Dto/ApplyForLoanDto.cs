using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Domain.Entity;
using UCP.Domain.Enum;

namespace UCP.Application.Dto
{
    public class ApplyForLoanDto
    {
        public Guid Id { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? DateCreated { get; set; }
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
