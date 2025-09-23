using Microsoft.EntityFrameworkCore;
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

            // Helt manuell mappning
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Credits = c.Credits,
                InstructorName = c.Instructor != null ?
                    $"{c.Instructor.FirstName} {c.Instructor.LastName}" : "TBD",
                EnrolledStudents = c.Enrollments?.Count ?? 0
            });
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return null;

            // Manuell mappning
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Credits = course.Credits,
                InstructorName = course.Instructor != null ?
                    $"{course.Instructor.FirstName} {course.Instructor.LastName}" : "TBD",
                EnrolledStudents = course.Enrollments?.Count ?? 0
            };
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            // Verify instructor exists
            var instructorExists = await _context.Instructors
                .AnyAsync(i => i.Id == createCourseDto.InstructorId);

            if (!instructorExists)
                throw new ArgumentException("Instructor not found");

            // Manuell mappning från DTO till Entity
            var course = new Course
            {
                Title = createCourseDto.Title,
                Description = createCourseDto.Description,
                Credits = createCourseDto.Credits,
                InstructorId = createCourseDto.InstructorId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            // Load related data for return
            await _context.Entry(course)
                .Reference(c => c.Instructor)
                .LoadAsync();

            // Manuell mappning tillbaka till DTO
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Credits = course.Credits,
                InstructorName = course.Instructor != null ?
                    $"{course.Instructor.FirstName} {course.Instructor.LastName}" : "TBD",
                EnrolledStudents = 0 // Nyskapat course har inga enrollments
            };
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

            // Manuell uppdatering av entity
            existingCourse.Title = updateCourseDto.Title;
            existingCourse.Description = updateCourseDto.Description;
            existingCourse.Credits = updateCourseDto.Credits;
            existingCourse.InstructorId = updateCourseDto.InstructorId;

            await _context.SaveChangesAsync();

            // Manuell mappning till DTO
            return new CourseDto
            {
                Id = existingCourse.Id,
                Title = existingCourse.Title,
                Description = existingCourse.Description,
                Credits = existingCourse.Credits,
                InstructorName = existingCourse.Instructor != null ?
                    $"{existingCourse.Instructor.FirstName} {existingCourse.Instructor.LastName}" : "TBD",
                EnrolledStudents = existingCourse.Enrollments?.Count ?? 0
            };
        }

        // Resten av metoderna förblir samma...
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