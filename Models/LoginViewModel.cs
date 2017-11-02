using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eureka.Models
{
    public class LoginViewModel : BaseEntity
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string lemail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string pwd { get; set; }
    }
}