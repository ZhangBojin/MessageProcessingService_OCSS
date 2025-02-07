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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=37.tcp.cpolar.top,10487;Database=CourseServices_OCSS;uid=sa;pwd=zz;TrustServerCertificate=True;");

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
