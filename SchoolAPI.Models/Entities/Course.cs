namespace SchoolAPI.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int InstructorId { get; set; }

        // Navigation properties
        public Instructor Instructor { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}