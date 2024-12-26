using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace webFullStackApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentSubjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/studentSubjects
        [HttpPost]
        public async Task<ActionResult> AddStudentSubject([FromBody] StudentSubject studentSubject)
        {
            if (studentSubject == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _unitOfWork.StudentSubjects.AddAsync(studentSubject);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction(nameof(GetStudentSubjectsByStudentId), new { studentId = studentSubject.StudentId }, studentSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/studentSubjects/{studentId}
        [HttpGet("{studentId}")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetStudentSubjectsByStudentId(int studentId)
        {
            try
            {
                var subjects = await _unitOfWork.StudentSubjects.GetSubjectsByStudentIdAsync(studentId);
                if (subjects == null || !subjects.Any())
                {
                    return NotFound();
                }
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/studentSubjects/{studentId}/{subjectId}
        [HttpDelete("{studentId}/{subjectId}")]
        public async Task<IActionResult> DeleteStudentSubject(int studentId, int subjectId)
        {
            try
            {
                await _unitOfWork.StudentSubjects.DeleteAsync(studentId, subjectId);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
