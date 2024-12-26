using Application.Service.Interface;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webFullStackApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // POST: api/subjects
        [HttpPost]

        public async Task<ActionResult> AddSubject(Subject subject)
        {
            try
            {
                // Call the service to add the subject
                await _subjectService.AddSubjectAsync(subject);
                return CreatedAtAction(nameof(GetSubjectById), new { id = subject.Id }, subject);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a bad request response
                return BadRequest(ex.Message);
            }
        }

        // GET: api/subjects
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Subject>>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }

        // GET: api/subjects/{id}
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<Subject>> GetSubjectById(int id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectByIdAsync(id);
                return Ok(subject);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/subjects/{id}
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateSubject(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return BadRequest("Subject ID mismatch");
            }

            try
            {
                await _subjectService.UpdateSubjectAsync(subject);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/subjects/{id}
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                await _subjectService.DeleteSubjectAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}


