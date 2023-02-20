using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using UCP.Domain.Enum;

namespace UCP.Domain.Entity
{
    public class Member : IdentityUser
    {                   
        public DateTime DateJoined { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public string Email { get; set; }
        public string Surname{ get; set; }
        public string OtherName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? AccountNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public ApplyForLoan ApplyForLoan { get; set; }
        public Guid ApplyForLoanId { get; set; }
    } 
}