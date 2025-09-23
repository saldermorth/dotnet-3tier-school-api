using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Business.Interfaces;
using SchoolAPI.Models.DTOs;

namespace SchoolAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        /// <summary>
        /// Get student by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound($"Student with ID {id} not found");

            return Ok(student);
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto createStudentDto)
        {
            try
            {
                var student = await _studentService.CreateStudentAsync(createStudentDto);
                return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing student
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            try
            {
                var student = await _studentService.UpdateStudentAsync(id, updateStudentDto);
                if (student == null)
                    return NotFound($"Student with ID {id} not found");

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a student
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result)
                return NotFound($"Student with ID {id} not found");

            return NoContent();
        }

        /// <summary>
        /// Search students by name
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> SearchStudents([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest("Search term cannot be empty");

            var students = await _studentService.SearchStudentsByNameAsync(searchTerm);
            return Ok(students);
        }
    }
}