using Microsoft.EntityFrameworkCore;
using SEProject.Models;

namespace SEProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ClassifiedFile> ClassifiedFiles { get; set; }
        public DbSet<PoliceStation> PoliceStations { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
