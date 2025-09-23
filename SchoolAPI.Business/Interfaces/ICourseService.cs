using SchoolAPI.Models.DTOs;

namespace SchoolAPI.Business.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task<CourseDto?> UpdateCourseAsync(int id, CreateCourseDto updateCourseDto);
        Task<bool> DeleteCourseAsync(int id);
        Task<bool> EnrollStudentAsync(int courseId, int studentId);
        Task<bool> UnenrollStudentAsync(int courseId, int studentId);
    }
}