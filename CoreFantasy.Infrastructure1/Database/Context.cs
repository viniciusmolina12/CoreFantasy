using CoreFantasy.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace CoreFantasy.Infrastructure.Database
{
    public class CoreFantasyDbContext : DbContext
    {
        public CoreFantasyDbContext(DbContextOptions<CoreFantasyDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CoreFantasyDbContext).Assembly
            );
        }
    }
}
