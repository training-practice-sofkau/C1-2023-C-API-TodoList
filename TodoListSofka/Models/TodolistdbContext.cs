using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoListSofka.Models;

public partial class TodolistdbContext : DbContext
{
    public TodolistdbContext()
    {
    }

    public TodolistdbContext(DbContextOptions<TodolistdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Todoitem> Todoitems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todoitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TODOITEM__3214EC07FE722D73");

            entity.ToTable("TODOITEMS");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Responsible)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
