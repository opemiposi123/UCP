using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCP.Domain.Entity
{
    public  class Loan : BaseEntity
    {
        public string LoanName { get; set; }
        public decimal InterestRate { get; set; }
    }
}
