using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class TreeNodeDbContext : DbContext
    {
        public TreeNodeDbContext(DbContextOptions<TreeNodeDbContext> options)
            : base(options)
        {
        }

        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<Journal> Journals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreeNode>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name);
                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}