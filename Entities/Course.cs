using System.ComponentModel.DataAnnotations;

namespace ProblemAssignmnet2_SruthiKamisetti.Entities
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage ="Course Name is required")]
        public string? CourseName { get; set; }

        [Required(ErrorMessage = "Instructor Name is required")]
        public string? Instructor {  get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Room Number is required")]
        [RegularExpression("^\\d[A-Z]\\d{2}$",
            ErrorMessage = "Room number must be in eg:3G15 format")]
        public string? RoomNumber {  get; set; }

        public ICollection<Student>? Students { get; set; }

    }
}
