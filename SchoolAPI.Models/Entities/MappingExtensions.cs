using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.Entities;

namespace SchoolAPI.Business.Extensions
{
    public static class MappingExtensions
    {
        // Student mappings
        public static StudentDto ToDto(this Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                EnrollmentDate = student.EnrollmentDate
            };
        }

        public static Student ToEntity(this CreateStudentDto dto)
        {
            return new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                EnrollmentDate = DateTime.UtcNow
            };
        }

        public static void UpdateEntity(this UpdateStudentDto dto, Student student)
        {
            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;
            student.DateOfBirth = dto.DateOfBirth;
            // Note: Don't update EnrollmentDate and Id
        }

        // Instructor mappings
        public static InstructorDto ToDto(this Instructor instructor)
        {
            return new InstructorDto
            {
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Email = instructor.Email,
                HireDate = instructor.HireDate,
                Department = instructor.Department,
                CoursesCount = instructor.Courses?.Count ?? 0
            };
        }

        public static Instructor ToEntity(this CreateInstructorDto dto)
        {
            return new Instructor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Department = dto.Department,
                HireDate = DateTime.UtcNow
            };
        }

        public static void UpdateEntity(this CreateInstructorDto dto, Instructor instructor)
        {
            instructor.FirstName = dto.FirstName;
            instructor.LastName = dto.LastName;
            instructor.Email = dto.Email;
            instructor.Department = dto.Department;
            // Note: Don't update HireDate and Id
        }

        // Course mappings
        public static CourseDto ToDto(this Course course)
        {
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

        public static Course ToEntity(this CreateCourseDto dto)
        {
            return new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Credits = dto.Credits,
                InstructorId = dto.InstructorId
            };
        }

        public static void UpdateEntity(this CreateCourseDto dto, Course course)
        {
            course.Title = dto.Title;
            course.Description = dto.Description;
            course.Credits = dto.Credits;
            course.InstructorId = dto.InstructorId;
            // Note: Don't update Id
        }
    }
}