using ProblemAssignmnet2_SruthiKamisetti.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProblemAssignmnet2_SruthiKamisetti.Models
{
    public class CourseStudents
    {
        public List<Student>? Students { get; set; }
        public Course? ActiveCourse { get; set; }
        public Student? Student { get; set; }
        public ResponseModel? StudentResponse {  get; set; }
    }

}