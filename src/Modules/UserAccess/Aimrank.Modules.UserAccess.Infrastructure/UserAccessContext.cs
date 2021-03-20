using Aimrank.Modules.UserAccess.Domain.Friendships;
using Aimrank.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Aimrank.Database.Migrator")]
[assembly:InternalsVisibleTo("Aimrank.Web")]

namespace Aimrank.Modules.UserAccess.Infrastructure
{
    internal class UserAccessContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        
        public UserAccessContext(DbContextOptions<UserAccessContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}