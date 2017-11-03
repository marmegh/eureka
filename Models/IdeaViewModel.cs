using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eureka.Models
{
    public class IdeaViewModel : BaseEntity
    {
        [Required]
        public string idea { get; set; }
    }
}