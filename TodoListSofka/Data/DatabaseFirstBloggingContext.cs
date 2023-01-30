using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoListSofka.Model;

namespace TodoListSofka.Data;

public partial class DatabaseFirstBloggingContext : DbContext
{
    public DatabaseFirstBloggingContext()
    {
    }
    public DatabaseFirstBloggingContext(DbContextOptions<DatabaseFirstBloggingContext> options)
        : base(options)
    {
    }
    public DbSet<ToDoItem> ToDoItems { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=DatabaseFirst.Blogging;Trusted_Connection=True;TrustServerCertificate=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__ToDoItem__727E838BE669966F");

            entity.ToTable("ToDoItem");

            entity.Property(e => e.ItemId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Responsible).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
