﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCP.Application.Dto
{
    public class ChangePasswordModelDto
    {
        [Required]
        public string? CurrentPassword { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string? NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string? PasswordConfirm { get; set; }
    }
}
