using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Business.Interfaces;
using SchoolAPI.Models.DTOs;

namespace SchoolAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound($"Course with ID {id} not found");

            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto createCourseDto)
        {
            try
            {
                var course = await _courseService.CreateCourseAsync(createCourseDto);
                return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CreateCourseDto updateCourseDto)
        {
            try
            {
                var course = await _courseService.UpdateCourseAsync(id, updateCourseDto);
                if (course == null)
                    return NotFound($"Course with ID {id} not found");

                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (!result)
                return NotFound($"Course with ID {id} not found");

            return NoContent();
        }

        /// <summary>
        /// Enroll a student in a course
        /// </summary>
        [HttpPost("{courseId}/enroll/{studentId}")]
        public async Task<IActionResult> EnrollStudent(int courseId, int studentId)
        {
            var result = await _courseService.EnrollStudentAsync(courseId, studentId);
            if (!result)
                return BadRequest("Unable to enroll student. Check if course and student exist and student is not already enrolled.");

            return Ok("Student enrolled successfully");
        }

        /// <summary>
        /// Unenroll a student from a course
        /// </summary>
        [HttpDelete("{courseId}/unenroll/{studentId}")]
        public async Task<IActionResult> UnenrollStudent(int courseId, int studentId)
        {
            var result = await _courseService.UnenrollStudentAsync(courseId, studentId);
            if (!result)
                return NotFound("Enrollment not found");

            return Ok("Student unenrolled successfully");
        }
    }
}