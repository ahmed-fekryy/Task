using Application.Service.Interface;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webFullStackApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/students
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logger here)
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/students/{id}
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/students
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student data is required.");
            }

            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(student.Name))
                {
                    return BadRequest("Student name is required.");
                }

                await _studentService.AddStudentAsync(student);
                return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest("Student ID mismatch");
            }

            try
            {
                await _studentService.UpdateStudentAsync(student);

                // Return the updated student
                return Ok(student); // 200 OK with the updated student data
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // 404 Not Found if the student doesn't exist
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/students/{id}
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }

                await _studentService.DeleteStudentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

