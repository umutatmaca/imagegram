using Imagegram.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Imagegram.Infrastructure.Database
{
    public class ImagegramContext : DbContext
    {
        private const string SchemaName = "App";
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ImagegramContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(builder =>
            {
                builder.ToTable("Accounts", SchemaName);
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name);
            });

            modelBuilder.Entity<Post>(builder =>
            {
                builder.ToTable("Posts", SchemaName);
                builder.HasKey(x => x.Id);
                builder.Property(x => x.CreatedAt);
                builder.Property(x => x.ImageUrl);
                builder.HasOne(x => x.Creator).WithMany(x => x.Posts).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comment>(builder =>
            {
                builder.ToTable("Comments", SchemaName);
                builder.HasKey(x => x.Id);
                builder.Property(x => x.CreatedAt);
                builder.Property(x => x.Content);
                builder.HasOne(x => x.Creator).WithMany(x => x.Comments).OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(x => x.Post).WithMany(x => x.Comments);
            });
        }
    }
}
