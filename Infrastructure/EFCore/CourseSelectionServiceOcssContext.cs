using MessageProcessingService_OCSS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageProcessingService_OCSS.Infrastructure.EFCore;

public class CourseSelectionServiceOcssContext(DbContextOptions<CourseSelectionServiceOcssContext> options)
    : DbContext(options)
{
    public virtual DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}