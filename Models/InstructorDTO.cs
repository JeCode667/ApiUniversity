namespace ApiUniversity.Models;

public class InstructorDTO
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public DateTime HireDate { get; set; }

    // Default constructor
    public InstructorDTO() { }

    public InstructorDTO(Instructor instructor)
    {
        Id = instructor.Id;
        LastName = instructor.LastName;
        FirstName = instructor.FirstName;
        HireDate = instructor.HireDate;

    }
}