using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eureka.Models{
    public class Idea : BaseEntity
    {
        // ideaid - Primary Key
        public int ideaid { get; set; }
        //idea
        public string idea { get; set; }
        // originator - userID
        public User origin { get; set; }
        public int userID { get; set; }
        // created_at - DATETIME
        public DateTime created_at { get; set; }
        // updated_at - DATETIME
        public DateTime updated_at { get; set; }
        public List<Like> liked { get; set; }
        public Idea()
        {
            liked = new List<Like>();
        }
    }
}

