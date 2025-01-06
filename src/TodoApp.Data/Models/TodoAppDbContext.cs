using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Models.Common;

namespace TodoApp.Data.Models;

public class TodoAppDbContext : DbContext
{
    public DbSet<TodoList> TodoList { get; set; }

    public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Description)
                .HasMaxLength(500);
            
            entity.Property(e => e.IsCompleted)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        
        base.OnModelCreating(modelBuilder);
    }
}
