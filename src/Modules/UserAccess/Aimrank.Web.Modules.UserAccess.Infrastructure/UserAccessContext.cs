using Aimrank.Web.Modules.UserAccess.Domain.Friendships;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Aimrank.Web.Database.Migrator")]

namespace Aimrank.Web.Modules.UserAccess.Infrastructure
{
    internal class UserAccessContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        
        public UserAccessContext(DbContextOptions<UserAccessContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}