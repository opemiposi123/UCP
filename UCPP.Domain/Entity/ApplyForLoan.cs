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
        public string LoanName { get; set; }
        public int LoanTerm { get; set; }
        public string Email { get; set; }
        public DateTime? LoanStartDate { get; set; }
        public DateTime? LoanEndDate { get; set; } 
        public PaymentFrequency PaymentFrequency { get; set; }
        public string? Purpose { get; set; }
        //public Collateral Collateral { get; set; }
        //public Guid CollateralId{ get; set; }
        //public Member MemberId { get; set; }
        //[NotMapped]
        //public ICollection<Member> Member { get; set; } = new HashSet<Member>();
        public Member Member { get; set; }
        public Member MemberId { get; set; }
    }
}
