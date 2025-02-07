namespace MessageProcessingService_OCSS.Domain.Entities;

public class Enrollment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CoursesId { get; set; } 
    public DateTime EnrollmentDate { get; set; }
    public double? Grade { get; set; }
    public bool? IsPass { get; set; }
}