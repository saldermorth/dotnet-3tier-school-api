namespace SchoolAPI.Models.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }

        // Navigation properties
        public List<Course> Courses { get; set; } = new();
    }
}