using System;
using System.Collections.Generic;
using LogServer_OCSS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogServer_OCSS.Infrastructure.EFCore;

public partial class LogServiceOcssContext : DbContext
{
    public LogServiceOcssContext()
    {
    }

    public LogServiceOcssContext(DbContextOptions<LogServiceOcssContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Log> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:LogServiceConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
