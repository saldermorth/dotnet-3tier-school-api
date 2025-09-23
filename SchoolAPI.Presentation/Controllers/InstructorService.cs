using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Business.Interfaces;
using SchoolAPI.Models.DTOs;

namespace SchoolAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructors()
        {
            var instructors = await _instructorService.GetAllInstructorsAsync();
            return Ok(instructors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructor(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
                return NotFound($"Instructor with ID {id} not found");

            return Ok(instructor);
        }

        [HttpPost]
        public async Task<ActionResult<InstructorDto>> CreateInstructor(CreateInstructorDto createInstructorDto)
        {
            try
            {
                var instructor = await _instructorService.CreateInstructorAsync(createInstructorDto);
                return CreatedAtAction(nameof(GetInstructor), new { id = instructor.Id }, instructor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstructor(int id, CreateInstructorDto updateInstructorDto)
        {
            try
            {
                var instructor = await _instructorService.UpdateInstructorAsync(id, updateInstructorDto);
                if (instructor == null)
                    return NotFound($"Instructor with ID {id} not found");

                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var result = await _instructorService.DeleteInstructorAsync(id);
            if (!result)
                return NotFound($"Instructor with ID {id} not found or has assigned courses");

            return NoContent();
        }
    }
}