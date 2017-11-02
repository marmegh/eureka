using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eureka.Models
{
    public class User : BaseEntity
    {
        public int userID { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        // public List<Wedding> weddings { get; set; }
        // public List<RSVP> rsvped { get; set; }
        // public User()
        // {
        //     weddings = new List<Wedding>();
        //     rsvped = new List<RSVP>();
        // }
    }
}