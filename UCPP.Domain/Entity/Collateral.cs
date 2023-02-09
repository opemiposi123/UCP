using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCP.Domain.Entity
{
    public class Collateral
    {

        public string Location { get; set; }
        public Guid Id { get; set; }
        public string CollateralName { get; set; }
        public decimal CollateralWorth { get; set; }
    }
}
