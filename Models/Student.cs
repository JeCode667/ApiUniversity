namespace ApiUniversity.Models;

public class Student
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Email { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();

    // Default constructor
    public Student() { }

    public Student(StudentDTO studentDTO)
    {
        Id = studentDTO.Id;
        LastName = studentDTO.LastName;
        FirstName = studentDTO.FirstName;
        Email = studentDTO.Email;
        EnrollmentDate = studentDTO.EnrollmentDate;

    }
}