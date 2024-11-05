using ApiUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/Student")]
public class StudentController : ControllerBase
{
private readonly UniversityContext _context;
public StudentController(UniversityContext context)
{
_context = context;
}


// GET: api/item
[HttpGet]
[SwaggerOperation(
Summary = "Get all Students",
Description = "Returns all Students and their characteristics")
]
[SwaggerResponse(StatusCodes.Status200OK, "Students found", typeof(Student))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Students not found")]
public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
{
// Get items
var Students = _context.Students;
Console.WriteLine(Students);
return await Students.ToListAsync();
}

// GET: api/Student/2
[HttpGet("{id}")]
[SwaggerOperation(
Summary = "Get a Student by id",
Description = "Returns a specific Student targeted by its identifier")
]
[SwaggerResponse(StatusCodes.Status200OK, "Student found", typeof(Student))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Student not found")]
public async Task<ActionResult<Student>> GetStudent([SwaggerParameter("The unique identifier of the Student", Required = true)] int id)
{
// Find a specific item
// SingleAsync() throws an exception if no item is found (which is possible, depending on id)
// SingleOrDefaultAsync() is a safer choice here
var Student = await _context.Students.SingleOrDefaultAsync(t => t.Id == id);


if (Student == null)
return NotFound();


return Student;
}

// POST: api/item
[HttpPost]
[SwaggerOperation(
Summary = "Create a Student",
Description = "Create a Student by adding its characteristics")
]
[SwaggerResponse(StatusCodes.Status200OK, "Student created", typeof(Student))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Student not created", typeof(Student))]
public async Task<ActionResult<Student>> PostStudent([SwaggerParameter("The new Student", Required = true)] Student Student)
{
_context.Students.Add(Student);
await _context.SaveChangesAsync();


return CreatedAtAction(nameof(GetStudent), new { id = Student.Id }, Student);
}

// PUT: api/item/2
[HttpPut("{id}")]
[SwaggerOperation(
Summary = "Update a Student by id",
Description = "Updates a specific Student targeted by its identifier")
]
[SwaggerResponse(StatusCodes.Status200OK, "Student updated", typeof(Student))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Student not found")]
public async Task<IActionResult> PutStudent([SwaggerParameter("The unique identifier of the Student", Required = true)] int id, [SwaggerParameter("The uptated Student", Required = true)] Student Student)
{
if (id != Student.Id)
return BadRequest();


_context.Entry(Student).State = EntityState.Modified;


try
{
await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
if (!_context.Students.Any(m => m.Id == id))
return NotFound();
else
throw;
}


return NoContent();
}

// DELETE: api/item/2
[HttpDelete("{id}")]
[SwaggerOperation(
Summary = "Delete a Student by id",
Description = "Deletes a specific Student targeted by its identifier")
]
[SwaggerResponse(204, "Student deleted", typeof(Student))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Student not found")]
public async Task<IActionResult> DeleteStudent([SwaggerParameter("The unique identifier of the Student", Required = true)] int id)
{
var Student = await _context.Students.FindAsync(id);


if (Student == null)
return NotFound();


_context.Students.Remove(Student);
await _context.SaveChangesAsync();


return NoContent();
}


}
