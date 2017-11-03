using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace eureka.Models
{
    public class RegistrationViewModel: BaseEntity
    {
        [Required]
        [Display(Name = "Name")]
        public string first { get; set; }

        [Required]
        [Display(Name = "alias")]
        public string last { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(password))]
        public string PWC { get; set; }
    }
}