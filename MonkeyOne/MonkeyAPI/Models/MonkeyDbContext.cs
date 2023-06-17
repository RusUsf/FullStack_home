using Npgsql;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Npgsql.EntityFrameworkCore.PostgreSQL.Design;
namespace MonkeyAPI.Models;

public partial class MonkeyDbContext : DbContext
{
    public MonkeyDbContext()
    {
    }

    public MonkeyDbContext(DbContextOptions<MonkeyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Monkeytable> Monkeytables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=burner;Password=burner;Host=localhost;Port=5432;Database=monkey_db;Search Path=public;CommandTimeout =120;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Create a new method.
        void ConfigureMonkeytable(ModelBuilder builder)
        {
            // Use the builder variable in the new method.
            builder.UseSerialColumns();
        }

        // Call the new method.
        ConfigureMonkeytable(modelBuilder);


        modelBuilder.Entity<Monkeytable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("monkeytable_pkey");

            entity.ToTable("monkeytable");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Tip)
                .HasColumnType("character varying")
                .HasColumnName("tip");
            
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
