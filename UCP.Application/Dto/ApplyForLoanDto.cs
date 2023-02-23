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
        public Guid LoanId { get; set; }
        public int LoanTerm { get; set; }
        public PaymentFrequency PaymentFrequency { get; set; }
        public string? Purpose { get; set; }


    }
}
