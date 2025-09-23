using Microsoft.EntityFrameworkCore;
using SchoolAPI.Business.Extensions;
using SchoolAPI.Business.Interfaces;
using SchoolAPI.Data.Context;
using SchoolAPI.Models.DTOs;

namespace SchoolAPI.Business.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly SchoolDbContext _context;

        public InstructorService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync()
        {
            var instructors = await _context.Instructors
                .Include(i => i.Courses)
                .OrderBy(i => i.LastName)
                .ThenBy(i => i.FirstName)
                .ToListAsync();

            return instructors.Select(i => i.ToDto());
        }

        public async Task<InstructorDto?> GetInstructorByIdAsync(int id)
        {
            var instructor = await _context.Instructors
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(i => i.Id == id);

            return instructor?.ToDto();
        }

        public async Task<InstructorDto> CreateInstructorAsync(CreateInstructorDto createInstructorDto)
        {
            var instructor = createInstructorDto.ToEntity();

            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            return instructor.ToDto();
        }

        public async Task<InstructorDto?> UpdateInstructorAsync(int id, CreateInstructorDto updateInstructorDto)
        {
            var existingInstructor = await _context.Instructors
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (existingInstructor == null)
                return null;

            updateInstructorDto.UpdateEntity(existingInstructor);
            await _context.SaveChangesAsync();

            return existingInstructor.ToDto();
        }

        public async Task<bool> DeleteInstructorAsync(int id)
        {
            var instructor = await _context.Instructors
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (instructor == null)
                return false;

            // Check if instructor has courses
            if (instructor.Courses.Any())
                return false; // Cannot delete instructor with courses

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}