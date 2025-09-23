using Microsoft.EntityFrameworkCore;
using SchoolAPI.Business.Extensions;
using SchoolAPI.Business.Interfaces;
using SchoolAPI.Data.Context;
using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.Entities;

namespace SchoolAPI.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly SchoolDbContext _context;

        public CourseService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .OrderBy(c => c.Title)
                .ToListAsync();

            return courses.Select(c => c.ToDto());
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            return course?.ToDto();
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            // Verify instructor exists
            var instructorExists = await _context.Instructors
                .AnyAsync(i => i.Id == createCourseDto.InstructorId);

            if (!instructorExists)
                throw new ArgumentException("Instructor not found");

            var course = createCourseDto.ToEntity();

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            // Load related data for return
            await _context.Entry(course)
                .Reference(c => c.Instructor)
                .LoadAsync();

            return course.ToDto();
        }

        public async Task<CourseDto?> UpdateCourseAsync(int id, CreateCourseDto updateCourseDto)
        {
            var existingCourse = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCourse == null)
                return null;

            // Verify instructor exists if changing
            if (existingCourse.InstructorId != updateCourseDto.InstructorId)
            {
                var instructorExists = await _context.Instructors
                    .AnyAsync(i => i.Id == updateCourseDto.InstructorId);

                if (!instructorExists)
                    throw new ArgumentException("Instructor not found");
            }

            updateCourseDto.UpdateEntity(existingCourse);
            await _context.SaveChangesAsync();

            return existingCourse.ToDto();
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EnrollStudentAsync(int courseId, int studentId)
        {
            // Check if course and student exist
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == courseId);
            var studentExists = await _context.Students.AnyAsync(s => s.Id == studentId);

            if (!courseExists || !studentExists)
                return false;

            // Check if already enrolled
            var existingEnrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);

            if (existingEnrollment != null)
                return false;

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId,
                EnrollmentDate = DateTime.UtcNow
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnenrollStudentAsync(int courseId, int studentId)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);

            if (enrollment == null)
                return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}