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
    public class MemberDto : BaseEntity
    {
        public string Email { get; set; }
        public string Surname { get; set; }
        public string OtherName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string? AccountNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }
    }
}
