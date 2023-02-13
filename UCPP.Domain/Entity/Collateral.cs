using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace UCP.Domain.Entity
{
    public class Collateral
    {

        public string Location { get; set; }
        public Guid Id { get; set; }
        public string CollateralName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CollateralWorth { get; set; }
    }
}
