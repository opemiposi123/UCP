using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Domain.Entity;

namespace UCP.Application.Dto
{
    public class LoanDto 
    {
        public Guid Id { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime Datejoined { get; set; }
        public string CreateBy { get; set; }
        public string LoanName { get; set; }
        public decimal InterestRate { get; set; }
    }
}
