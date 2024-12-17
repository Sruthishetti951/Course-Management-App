using System.ComponentModel.DataAnnotations;

namespace ProblemAssignmnet2_SruthiKamisetti.Entities
{

    public enum Status
    {
        ConfirmationMessageNotSent,
        ConfirmationMessageSent,
        EnrollmentConfirmed,
        EnrollmentDeclined
    }
    public class Student
    {
        public int? StudentId { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        public string? StudentName { get; set; }

        [Required(ErrorMessage = "Student email is required")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
            ErrorMessage = "Invalid Email Pattern")]
        public string? StudentEmail { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public Status Status { get; set; }=Status.ConfirmationMessageNotSent;
        public int CourseId {  get; set; }
        public Course? Course {  get; set; }
    }
}