using Microsoft.EntityFrameworkCore;

namespace eureka.Models{
    public class eurekaContext: DbContext
    {
        public eurekaContext(DbContextOptions<eurekaContext> options) : base(options) {}
        public DbSet<User> users { get; set; }
        public DbSet<Idea> ideas { get; set; }
        public DbSet<Like> likes { get; set; }
    }
}