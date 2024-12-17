using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemAssignmnet2_SruthiKamisetti.Entities;
using ProblemAssignmnet2_SruthiKamisetti.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace ProblemAssignmnet2_SruthiKamisetti.Controllers
{
    public class CourseController : BaseController
    {
        private CourseDBContext _courseDBContext;
        private readonly IConfiguration _config;
        public CourseController(CourseDBContext courseDBContext, IConfiguration config)
        {
            _courseDBContext = courseDBContext;
            _config = config;
        }

        // Getting all the courses from DB
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            List<CourseList> allCourses = _courseDBContext.Courses.Select(c => new CourseList
            {
                CourseName = c.CourseName,
                Instructor = c.Instructor,
                StartDate = c.StartDate,
                RoomNumber = c.RoomNumber,
                CourseId = c.CourseId,
                StudentCount = c.Students.Count()
            }).ToList();
            return View("Index", allCourses);
        }

        // Add New course page
        [HttpGet("/Course")]
        public IActionResult GetAddNewCourse() {
            return View("AddCourse", new Course());
        }

        // Add New course submission
        [HttpPost("/Course/GetAddNewCourse")]
        public IActionResult PostAddNewCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseDBContext.Courses.Add(course);
                _courseDBContext.SaveChanges();
                TempData["LastActionMessage"] = "Course Created Successfully";
                TempData["isSuccess"] = true;
                return RedirectToAction("GetAllCourses", "Course");

            }
            return View("AddCourse", course);
        }

        // Update New course Page and pre-filling required data from DB
        [HttpGet("/Course{id}/edit")]

        public IActionResult EditCourse(int id)
        {
            var Crs = _courseDBContext.Courses.Find(id);
            return View("EditCourse", Crs);
        }

        // Update New course Page and with updated data into DB
        [HttpPost("/Course/{id}/edit")]
        public IActionResult PostEditCourse(Course course, int id)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _courseDBContext.Courses.Update(course);
                _courseDBContext.SaveChanges();
                TempData["LastActionMessage"] = "Course Updated Successfully";
                TempData["isSuccess"] = true;
                return RedirectToAction("GetAllCourses", "Course");
            }
            return View("EditCourse", course);
        }

        // Get Mange course Page with list of student enrolled and email statistics
        [HttpGet("/Course{id}/manage")]

        public IActionResult ManageCourse(int id)
        {
            var CourseStudents = new CourseStudents
            {
                Students = _courseDBContext.Students.Where(s => s.CourseId == id).ToList(),
                ActiveCourse = _courseDBContext.Courses.Find(id)
            };
            return View("ManageCourse", CourseStudents);
        }

        // Adding Students into DB and showing an appropriate message to end user.
        [HttpPost("/Course/{id}/manage")]
        public IActionResult PostManageCourse(int id, CourseStudents courseStudents)
        {
            TempData["LastActionMessage"] = null;
            if (ModelState.IsValid)
            {
                bool studentExists = _courseDBContext.Students
                    .Any(s => s.CourseId == id && s.StudentEmail == courseStudents.Student.StudentEmail);

                if (studentExists)
                {
                    TempData["LastActionMessage"] = "This student is already enrolled in this course.";
                    TempData["isSuccess"] = false;

                    var CourseStudents = new CourseStudents
                    {
                        Students = _courseDBContext.Students.Where(s => s.CourseId == id).ToList(),
                        ActiveCourse = _courseDBContext.Courses.Find(id)
                    };
                    return View("ManageCourse", CourseStudents);
                }

                courseStudents.Student.CourseId = id;
                _courseDBContext.Students.Add(courseStudents.Student);
                _courseDBContext.SaveChanges();

                TempData["LastActionMessage"] = "Student created successfully";
                TempData["isSuccess"] = true;
                return RedirectToAction("ManageCourse", "Course", new { id = id });
            }

            var CourseStudentsInvalid = new CourseStudents
            {
                Students = _courseDBContext.Students.Where(s => s.CourseId == id).ToList(),
                ActiveCourse = _courseDBContext.Courses.Find(id)
            };
            return View("ManageCourse", CourseStudentsInvalid);
        }

        // Sending Email to Students which are not sent before and updating status into DB along with status message.
        [HttpPost("/Email/{id}/manage")]
        public async Task<IActionResult> SendConfirmationEmail(int id)
        {
            var crs = await _courseDBContext.Courses.FindAsync(id);
            string fromAddress = "svsudowindo@gmail.com";
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromAddress, "dfoelyqscvqjpttz"),
                EnableSsl = true,
            };
            var url = $"{this.Request.Scheme}://{this.Request.Host}";

            // Get students who need to receive the confirmation email
            List<Student> students = await _courseDBContext.Students
                .Where(obj => obj.CourseId == id && obj.Status == Status.ConfirmationMessageNotSent)
                .GroupBy(obj => obj.StudentEmail)
                .Select(group => group.First())
                .ToListAsync();

            try
            {
                if (students.Count > 0)
                {
                    TempData["LastActionMessage"] = "Sending Email ....";
                    TempData["isSuccess"] = true;
                    // Loop through the students and send confirmation emails
                    foreach (var student in students)
                    {
                        var mailMessage = new MailMessage()
                        {
                            From = new MailAddress(fromAddress),
                            Subject = $"Enrollment confirmation for {crs.CourseName} required",
                            IsBodyHtml = true,
                            Body = $"<h1>Hello {student.StudentName} :</h1>\r\n<p>Your request to enroll in the course {crs.CourseName} in room {crs.RoomNumber} starting {crs.StartDate} with instructor {crs.Instructor}</p>\r\n<p>We are pleased to have you in the course so if you could <a href=\"{url}/Course{id}/{student.StudentId}\">confirm your enrollment</a> as soon as possible that would be appreciated!</p>\r\n<p>Sincerely,</p>\r\n<p>The Course Manager</p>"
                        };
                        mailMessage.To.Add(student.StudentEmail);

                        student.Status = Status.ConfirmationMessageSent;

                        await smtpClient.SendMailAsync(mailMessage);

                        _courseDBContext.Students.Update(student);
                    }

                    await _courseDBContext.SaveChangesAsync();

                    TempData["LastActionMessage"] = "Email sent successfully";
                    TempData["isSuccess"] = true;
                }

            }
            catch (Exception ex)
            {
                TempData["LastActionMessage"] = "Something went wrong..please try again!";
                TempData["isSuccess"] = false;
            }

            return RedirectToAction("ManageCourse", "Course", new { id = id });
        }

        // This method is called when the user clicks link received over email
        [HttpGet("/Course{id}/{sid}")]

        public IActionResult GetConfirmationResponse(int id,int sid)
        {
            CourseStudents courseStudents = new CourseStudents
            {
                ActiveCourse=_courseDBContext.Courses.Find(id),
                Student=_courseDBContext.Students.Where(obj=>obj.CourseId==id && obj.StudentId==sid).FirstOrDefault()
            };
            if (courseStudents == null)
            {
                return NotFound();
            }
                return View("ConfirmationResponse", courseStudents);
        }

        // This method updates the status when user submits the response.
        [HttpPost("/Course/{id}/{sid}")]

        public IActionResult PostConfirmationResponse(int id, int sid, ResponseModel responseModel) 
        {
           Student student= _courseDBContext.Students.Where(obj => obj.CourseId == id && obj.StudentId == sid).FirstOrDefault();
            if (student==null)
            {
                return NotFound();
            }
            if (student!=null)
            {
                student.Status=responseModel.Response=="Yes"?Status.EnrollmentConfirmed:Status.EnrollmentDeclined;
                _courseDBContext.SaveChanges();
                return RedirectToAction("GetThankYouResponse", "Course");
            }
            return RedirectToAction("ConfirmationResponse", "Course", new { id= id, sid = sid});
        }

        // Thank you page
        [HttpGet("/Course/ThankYou")]
        public IActionResult GetThankYouResponse(int id) 
        {
           return View("ThankYouResponse");
        }

    }
}
