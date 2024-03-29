﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalStore.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email is mandatory")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name is mandatory")]
        [Display(Name = "User Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        public string Address { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}