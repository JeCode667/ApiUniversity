namespace ApiUniversity.Models;

public class DepartmentDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int InstructorId { get; set; }

    // Default constructor
    public DepartmentDTO() { }

    public DepartmentDTO(Department department)
    {
        Id = department.Id;
        Name = department.Name;
        InstructorId = department.InstructorId;
    }
}