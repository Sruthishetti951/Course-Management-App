using ProblemAssignmnet2_SruthiKamisetti.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProblemAssignmnet2_SruthiKamisetti.Models
{
    public class CourseList: Course
    {
        public int? StudentCount { get; set; }
    }

}