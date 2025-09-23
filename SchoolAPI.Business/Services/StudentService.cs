using Microsoft.EntityFrameworkCore;
using SchoolAPI.Business.Extensions;
using SchoolAPI.Business.Interfaces;
using SchoolAPI.Data.Context;
using SchoolAPI.Models.DTOs;

namespace SchoolAPI.Business.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext _context;

        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _context.Students
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToListAsync();

            // Manual mapping using extension methods
            return students.Select(s => s.ToDto());
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            return student?.ToDto();
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto)
        {
            var student = createStudentDto.ToEntity();

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student.ToDto();
        }

        public async Task<StudentDto?> UpdateStudentAsync(int id, UpdateStudentDto updateStudentDto)
        {
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (existingStudent == null)
                return null;

            // Manual mapping using extension method
            updateStudentDto.UpdateEntity(existingStudent);
            await _context.SaveChangesAsync();

            return existingStudent.ToDto();
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<StudentDto>> SearchStudentsByNameAsync(string searchTerm)
        {
            var students = await _context.Students
                .Where(s => s.FirstName.Contains(searchTerm) || s.LastName.Contains(searchTerm))
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToListAsync();

            return students.Select(s => s.ToDto());
        }
    }
}