using ApiUniversity.Data;
using ApiUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversity.Controllers;

[ApiController]
[Route("api/instructor")]
public class InstructorController : ControllerBase
{
    private readonly UniversityContext _context;

    public InstructorController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/instructor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetailedInstructorDTO>>> GetInstructors()
    {
        // Get instructors and related lists
        var instructors = _context.Instructors.Include(x => x.AdministeredDepartements).Include(x => x.Courses).Select(x => new DetailedInstructorDTO(x));
        return await instructors.ToListAsync();
    }

    // GET: api/instructor/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedInstructorDTO>> GetInstructor(int id)
    {
        // Find instructor and related list
        // SingleAsync() throws an exception if no instructor is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var instructor = await _context.Instructors.Include(x => x.AdministeredDepartements).Include(x => x.Courses).SingleOrDefaultAsync(t => t.Id == id);

        if (instructor == null)
        {
            return NotFound();
        }

        return new DetailedInstructorDTO(instructor);
    }

    // POST: api/instructor
    [HttpPost]
    public async Task<ActionResult<InstructorDTO>> PostInstructor(InstructorDTO instructorDTO)
    {
        Instructor instructor = new(instructorDTO);
        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInstructor), new { id = instructor.Id }, new InstructorDTO(instructor));
    }

    // PUT: api/instructor/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutInstructor(int id, InstructorDTO instructorDTO)
    {
        if (id != instructorDTO.Id)
        {
            return BadRequest();
        }

        Instructor instructor = new(instructorDTO);

        _context.Entry(instructor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Instructors.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/instructor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInstructor(int id)
    {
        var instructor = await _context.Instructors.FindAsync(id);

        if (instructor == null)
        {
            return NotFound();
        }

        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}