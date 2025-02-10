using System;
using System.Collections.Generic;
using MessageProcessingService_OCSS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageProcessingService_OCSS.Infrastructure.EFCore;

public partial class CourseServicesOcssContext : DbContext
{
    public CourseServicesOcssContext()
    {
    }

    public CourseServicesOcssContext(DbContextOptions<CourseServicesOcssContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseAvailability> CourseAvailabilities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:CourseServicesConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseAvailability>(entity =>
        {
            entity.ToTable("CourseAvailability");

            entity.HasIndex(e => e.CoursesId, "IX_CourseAvailability_CoursesId");

            entity.HasOne(d => d.Courses).WithMany(p => p.CourseAvailabilities).HasForeignKey(d => d.CoursesId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
