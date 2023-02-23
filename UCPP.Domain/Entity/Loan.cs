using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCP.Domain.Entity
{
    public  class Loan : BaseEntity
    {
        public string LoanName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }
        public ICollection<ApplyForLoan> LoanApplicant { get; set; } = new HashSet<ApplyForLoan>();
    }
}
