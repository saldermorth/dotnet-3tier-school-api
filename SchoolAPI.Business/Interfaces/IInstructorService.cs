using SchoolAPI.Models.DTOs;

namespace SchoolAPI.Business.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync();
        Task<InstructorDto?> GetInstructorByIdAsync(int id);
        Task<InstructorDto> CreateInstructorAsync(CreateInstructorDto createInstructorDto);
        Task<InstructorDto?> UpdateInstructorAsync(int id, CreateInstructorDto updateInstructorDto);
        Task<bool> DeleteInstructorAsync(int id);
    }
}