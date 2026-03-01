using Microsoft.EntityFrameworkCore;

namespace ForgeHire.Data
{
    public class DbConnection : DbContext {
        public DbConnection(DbContextOptions<DbConnection> options)
        : base(options)
        {
        }

        //DbSet
        public DbSet<User> Users { get; set; }
    }
}
