using SchoolAPI.Data.Context;
using SchoolAPI.Models.Entities;

namespace SchoolAPI.Data.Seeding
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(SchoolDbContext context)
        {
            // Kontrollera om data redan finns
            if (context.Students.Any())
                return;

            // Skapa lärare
            var instructors = new List<Instructor>
            {
                new Instructor
                {
                    FirstName = "Anna",
                    LastName = "Andersson",
                    Email = "anna.andersson@school.se",
                    Department = "Matematik",
                    HireDate = DateTime.UtcNow.AddYears(-5)
                },
                new Instructor
                {
                    FirstName = "Erik",
                    LastName = "Johansson",
                    Email = "erik.johansson@school.se",
                    Department = "Historia",
                    HireDate = DateTime.UtcNow.AddYears(-3)
                },
                new Instructor
                {
                    FirstName = "Maria",
                    LastName = "Karlsson",
                    Email = "maria.karlsson@school.se",
                    Department = "Engelska",
                    HireDate = DateTime.UtcNow.AddYears(-7)
                },
                new Instructor
                {
                    FirstName = "Lars",
                    LastName = "Nilsson",
                    Email = "lars.nilsson@school.se",
                    Department = "Fysik",
                    HireDate = DateTime.UtcNow.AddYears(-2)
                }
            };

            context.Instructors.AddRange(instructors);
            await context.SaveChangesAsync();

            // Skapa kurser
            var courses = new List<Course>
            {
                new Course
                {
                    Title = "Matematik 1",
                    Description = "Grundläggande matematik",
                    Credits = 100,
                    InstructorId = instructors[0].Id
                },
                new Course
                {
                    Title = "Matematik 2",
                    Description = "Fördjupad matematik",
                    Credits = 100,
                    InstructorId = instructors[0].Id
                },
                new Course
                {
                    Title = "Historia A",
                    Description = "Svensk historia",
                    Credits = 100,
                    InstructorId = instructors[1].Id
                },
                new Course
                {
                    Title = "Engelska 5",
                    Description = "Gymnasiengelska",
                    Credits = 100,
                    InstructorId = instructors[2].Id
                },
                new Course
                {
                    Title = "Fysik 1",
                    Description = "Grundläggande fysik",
                    Credits = 100,
                    InstructorId = instructors[3].Id
                },
                new Course
                {
                    Title = "Fysik 2",
                    Description = "Fördjupad fysik",
                    Credits = 100,
                    InstructorId = instructors[3].Id
                }
            };

            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();

            // Skapa studenter
            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Emma",
                    LastName = "Svensson",
                    Email = "emma.svensson@student.se",
                    DateOfBirth = new DateTime(2005, 3, 15),
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-6)
                },
                new Student
                {
                    FirstName = "Oskar",
                    LastName = "Lindberg",
                    Email = "oskar.lindberg@student.se",
                    DateOfBirth = new DateTime(2004, 8, 22),
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-8)
                },
                new Student
                {
                    FirstName = "Julia",
                    LastName = "Petersson",
                    Email = "julia.petersson@student.se",
                    DateOfBirth = new DateTime(2005, 1, 10),
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-4)
                },
                new Student
                {
                    FirstName = "Viktor",
                    LastName = "Magnusson",
                    Email = "viktor.magnusson@student.se",
                    DateOfBirth = new DateTime(2004, 11, 30),
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-10)
                },
                new Student
                {
                    FirstName = "Sara",
                    LastName = "Olsson",
                    Email = "sara.olsson@student.se",
                    DateOfBirth = new DateTime(2005, 6, 18),
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-3)
                },
                new Student
                {
                    FirstName = "Alex",
                    LastName = "Larsson",
                    Email = "alex.larsson@student.se",
                    DateOfBirth = new DateTime(2004, 9, 5),
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-7)
                }
            };

            context.Students.AddRange(students);
            await context.SaveChangesAsync();

            // Skapa kursregistreringar
            var enrollments = new List<Enrollment>
            {
                // Emma's kurser
                new Enrollment
                {
                    StudentId = students[0].Id,
                    CourseId = courses[0].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                    Grade = "A"
                },
                new Enrollment
                {
                    StudentId = students[0].Id,
                    CourseId = courses[2].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                    Grade = "B"
                },
                new Enrollment
                {
                    StudentId = students[0].Id,
                    CourseId = courses[3].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-1)
                },

                // Oskar's kurser
                new Enrollment
                {
                    StudentId = students[1].Id,
                    CourseId = courses[0].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-3),
                    Grade = "B"
                },
                new Enrollment
                {
                    StudentId = students[1].Id,
                    CourseId = courses[4].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                    Grade = "A"
                },

                // Julia's kurser
                new Enrollment
                {
                    StudentId = students[2].Id,
                    CourseId = courses[1].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-1)
                },
                new Enrollment
                {
                    StudentId = students[2].Id,
                    CourseId = courses[3].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-1),
                    Grade = "A"
                },

                // Viktor's kurser
                new Enrollment
                {
                    StudentId = students[3].Id,
                    CourseId = courses[2].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-4),
                    Grade = "C"
                },
                new Enrollment
                {
                    StudentId = students[3].Id,
                    CourseId = courses[4].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2)
                },
                new Enrollment
                {
                    StudentId = students[3].Id,
                    CourseId = courses[5].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-1)
                },

                // Sara's kurser
                new Enrollment
                {
                    StudentId = students[4].Id,
                    CourseId = courses[0].Id,
                   EnrollmentDate = DateTime.UtcNow.AddDays(-21)
                },
                new Enrollment
                {
                    StudentId = students[4].Id,
                    CourseId = courses[3].Id,
                    EnrollmentDate = DateTime.UtcNow.AddDays(-14) ,
                    Grade = "B"
                },

                // Alex's kurser
                new Enrollment
                {
                    StudentId = students[5].Id,
                    CourseId = courses[1].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                    Grade = "A"
                },
                new Enrollment
                {
                    StudentId = students[5].Id,
                    CourseId = courses[4].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-1)
                },
                new Enrollment
                {
                    StudentId = students[5].Id,
                    CourseId = courses[5].Id,
                    EnrollmentDate = DateTime.UtcNow.AddDays(-14)
                }
            };

            context.Enrollments.AddRange(enrollments);
            await context.SaveChangesAsync();

            Console.WriteLine($"Databas seedade med:");
            Console.WriteLine($"- {instructors.Count} lärare");
            Console.WriteLine($"- {courses.Count} kurser");
            Console.WriteLine($"- {students.Count} studenter");
            Console.WriteLine($"- {enrollments.Count} kursregistreringar");
        }
    }
}