using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCP.Application.Dto
{
    public class CountDto
    {
        public int? MembersCounts { get; set; }
        public int? LoansCount { get; set; }
        public int? ApplyForLoansCount { get; set; }
    }
}
