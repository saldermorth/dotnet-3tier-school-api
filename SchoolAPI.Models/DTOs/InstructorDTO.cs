namespace SchoolAPI.Models.DTOs
{
    public class InstructorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Department { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public int CoursesCount { get; set; }
    }

    public class CreateInstructorDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
}