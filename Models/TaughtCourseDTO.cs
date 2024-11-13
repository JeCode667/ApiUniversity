namespace ApiUniversity.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class TaughtCourseDTO
{

    public int Id { get; set; }
    public int CourseIds { get; set; }

    public TaughtCourseDTO() { }

    public TaughtCourseDTO(Course course)
    {
        Id = course.Id;
        Title = course.Title;
        Credits = course.Credits;        
        DepartmentId = course.DepartmentId;
    }

}