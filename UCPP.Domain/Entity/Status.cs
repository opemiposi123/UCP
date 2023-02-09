using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCP.Domain.Entity
{
    public class Status
    {
        public int StatusCode { get; set; }
        public decimal money { get; set; }
        public string Message { get; set; }
    }
}
