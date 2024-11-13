namespace ApiUniversity.Models;

public class DetailedInstructorDTO
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public DateTime HireDate { get; set; }
    public List<CourseDTO> Courses { get; set; } = new();
    public List<DepartmentDTO> AdministeredDepartements { get; set; } = new();

    // Default constructor
    public DetailedInstructorDTO() { }

    public DetailedInstructorDTO(Instructor instructor)
    {
        Id = instructor.Id;
        LastName = instructor.LastName;
        FirstName = instructor.FirstName;
        HireDate = instructor.HireDate;
        foreach(Course course in instructor.Courses){
            Courses.Add(new CourseDTO(course));
        }
        foreach(Department department in instructor.AdministeredDepartements){
            AdministeredDepartements.Add(new DepartmentDTO(department));
        }
    }
}