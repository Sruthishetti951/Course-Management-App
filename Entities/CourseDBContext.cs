using Microsoft.EntityFrameworkCore;
using ProblemAssignmnet2_SruthiKamisetti.Entities;
namespace ProblemAssignmnet2_SruthiKamisetti.Entities
{
    public class CourseDBContext:DbContext
    {
        public CourseDBContext(DbContextOptions<CourseDBContext>options):base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        // Seeding Initial Data into Course and Student table.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Course>().HasMany(c => c.Students).WithOne(e=>e.Course).HasForeignKey(e=>e.CourseId).IsRequired();
            
            modelBuilder.Entity<Course>().HasData(
                new Course()
                {
                    CourseId = 1,
                    CourseName = "ASP.NET",
                    Instructor = "Manny Singh",
                    StartDate = DateTime.Now.AddDays(30),
                    RoomNumber = "3G15"
                },
                new Course()
                {
                    CourseId = 2,
                    CourseName = "C#",
                    Instructor = "Sukhbir Tatla",
                    StartDate = DateTime.Now.AddDays(30),
                    RoomNumber = "3G15"
                },
                new Course()
                {
                    CourseId = 3,
                    CourseName = "DBMS",
                    Instructor = "John Smith",
                    StartDate = DateTime.Now.AddDays(30),
                    RoomNumber = "3G15"
                }
                );


            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    StudentId=1,
                    StudentName="Sruthi",
                    StudentEmail="Sruthi@gmail.com",
                    Status=Status.ConfirmationMessageNotSent,
                    CourseId=1
                },
                new Student()
                {
                    StudentId = 2,
                    StudentName = "Sai",
                    StudentEmail = "Sai@gmail.com",
                    Status = Status.ConfirmationMessageNotSent,
                    CourseId = 2
                },
                new Student()
                {
                    StudentId = 3,
                    StudentName = "Twinkle",
                    StudentEmail = "Twinkle@gmail.com",
                    Status = Status.ConfirmationMessageNotSent,
                    CourseId = 2
                },
                new Student()
                {
                    StudentId = 4,
                    StudentName = "Jothi",
                    StudentEmail = "Jothi@gmail.com",
                    Status = Status.ConfirmationMessageNotSent,
                    CourseId = 3
                });


        }

    }
}
