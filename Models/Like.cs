using System;

namespace eureka.Models
{
    public class Like : BaseEntity
    {
        public int likeid { get; set; }
        public int userID { get; set; }
        public User liker { get; set; }
        public int ideaid { get; set; }
        public Idea liked { get; set; }
        // created_at - DATETIME
        public DateTime created_at { get; set; }
        // updated_at - DATETIME
        public DateTime updated_at { get; set; }
    }
}