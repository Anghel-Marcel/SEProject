using Microsoft.EntityFrameworkCore;
using SEProject.DataAccess.Model;

namespace SEProject.DataAccess.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ClassifiedFile> ClassifiedFiles { get; set; }
        public DbSet<PoliceStation> PoliceStations { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
