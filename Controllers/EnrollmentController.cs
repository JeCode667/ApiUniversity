using ApiUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/enrollment")]
public class EnrollmentController : ControllerBase
{
private readonly UniversityContext _context;
public EnrollmentController(UniversityContext context)
{
_context = context;
}

// GET: api/item
[HttpGet]
[SwaggerOperation(
Summary = "Get all Enrollments",
Description = "Returns all Enrollments and their characteristics")
]
[SwaggerResponse(StatusCodes.Status200OK, "Enrollments found", typeof(Enrollment))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Enrollments not found")]
public async Task<ActionResult<IEnumerable<DetailedEnrollmentDTO>>> GetEnrollments()
{
// Get items
var enrollments = _context.Enrollments.Include(x => x.Student).Include(x => x.Course).Select(x => new DetailedEnrollmentDTO(x));
return await enrollments.ToListAsync();
}

// GET: api/Enrollment/2
[HttpGet("{id}")]
[SwaggerOperation(
Summary = "Get a Enrollment by id",
Description = "Returns a specific Enrollment targeted by its identifier")
]
[SwaggerResponse(StatusCodes.Status200OK, "Enrollment found", typeof(Enrollment))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Enrollment not found")]
public async Task<ActionResult<DetailedEnrollmentDTO>> GetEnrollment([SwaggerParameter("The unique identifier of the Enrollment", Required = true)] int id)
{
// Find a specific item
// SingleAsync() throws an exception if no item is found (which is possible, depending on id)
// SingleOrDefaultAsync() is a safer choice here
var Enrollment = await _context.Enrollments.Include(x => x.Student).Include(x => x.Course).SingleOrDefaultAsync(t => t.Id == id);


if (Enrollment == null)
return NotFound();


return new DetailedEnrollmentDTO(Enrollment);
}

// POST: api/item
[HttpPost]
[SwaggerOperation(
Summary = "Create a Enrollment",
Description = "Create a Enrollment by adding its characteristics")
]
[SwaggerResponse(StatusCodes.Status200OK, "Enrollment created", typeof(Enrollment))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Enrollment not created", typeof(Enrollment))]
public async Task<ActionResult<EnrollmentDTO>> PostEnrollment([SwaggerParameter("The new Enrollment", Required = true)] EnrollmentDTO EnrollmentDTO)
{
    Enrollment Enrollment = new(EnrollmentDTO);
_context.Enrollments.Add(Enrollment);
await _context.SaveChangesAsync();

return CreatedAtAction(nameof(GetEnrollment), new { id = Enrollment.Id }, new EnrollmentDTO(Enrollment));
}

// PUT: api/item/2
[HttpPut("{id}")]
[SwaggerOperation(
Summary = "Update a Enrollment by id",
Description = "Updates a specific Enrollment targeted by its identifier")
]
[SwaggerResponse(StatusCodes.Status200OK, "Enrollment updated", typeof(Enrollment))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Enrollment not found")]
public async Task<IActionResult> PutEnrollment([SwaggerParameter("The unique identifier of the Enrollment", Required = true)] int id, [SwaggerParameter("The uptated Enrollment", Required = true)] DetailedEnrollmentDTO DetailedEnrollmentDTO)
{
if (id != DetailedEnrollmentDTO.Id)
return BadRequest();

Enrollment Enrollment = new(DetailedEnrollmentDTO);

_context.Entry(Enrollment).State = EntityState.Modified;


try
{
await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
if (!_context.Enrollments.Any(m => m.Id == id))
return NotFound();
else
throw;
}


return NoContent();
}

// DELETE: api/item/2
[HttpDelete("{id}")]
[SwaggerOperation(
Summary = "Delete a Enrollment by id",
Description = "Deletes a specific Enrollment targeted by its identifier")
]
[SwaggerResponse(204, "Enrollment deleted", typeof(Enrollment))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Enrollment not found")]
public async Task<IActionResult> DeleteEnrollment([SwaggerParameter("The unique identifier of the Enrollment", Required = true)] int id)
{
var Enrollment = await _context.Enrollments.FindAsync(id);


if (Enrollment == null)
return NotFound();


_context.Enrollments.Remove(Enrollment);
await _context.SaveChangesAsync();


return NoContent();
}


}
