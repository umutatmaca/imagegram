using Imagegram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

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
                builder.HasMany(x => x.Posts)
                       .WithOne(x => x.Creator)
                       .HasForeignKey("FK_Post_Creator")
                       .OnDelete(DeleteBehavior.Cascade);
                builder.HasMany(x => x.Comments)
                       .WithOne(x => x.Creator)
                       .HasForeignKey("FK_Comment_Creator")
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Post>(builder =>
            {
                builder.ToTable("Posts", SchemaName);
                builder.HasKey(x => x.Id);
                builder.Property(x => x.CreatedAt);
                builder.Property(x => x.ImageUrl);
                builder.Property<Guid>("FK_Post_Creator");
                builder.HasOne(x => x.Creator)
                       .WithMany(x => x.Posts)
                       .HasForeignKey("FK_Post_Creator");
                builder.HasMany(x => x.Comments)
                       .WithOne(x => x.Post)
                       .HasForeignKey("FK_Comment_Post")
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comment>(builder =>
            {
                builder.ToTable("Comments", SchemaName);
                builder.HasKey(x => x.Id);
                builder.Property(x => x.CreatedAt);
                builder.Property(x => x.Content);
                builder.Property<Guid>("FK_Comment_Post");
                builder.Property<Guid>("FK_Comment_Creator");
                builder.HasOne(x => x.Post)
                       .WithMany(x => x.Comments)
                       .HasForeignKey("FK_Comment_Post");
                builder.HasOne(x => x.Creator)
                       .WithMany()
                       .HasForeignKey("FK_Comment_Creator");
            });
        }
    }
}
