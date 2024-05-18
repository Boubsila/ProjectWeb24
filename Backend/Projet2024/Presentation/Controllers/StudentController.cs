using BusinessLayer;
using Domaine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Student
        [HttpGet]
        //[Authorize(Roles = "2")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }


        //Get student by Id

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            var student = await _studentService.GetStudentById(studentId);
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound();
            }
        }

        //Get course for student 

        [HttpGet("GetCoursesForStudent/{studentId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesForStudent(int studentId)
        {
            var courses = await _studentService.GetCoursesForStudent(studentId);
            if (courses == null)
            {
                return NotFound();
            }
            return Ok(courses);
        }

        // Enroll student in course 'check'

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudentInCourse(int studentId, int courseId)
        {
            try
            {
                await _studentService.EnrollStudentInCourse(studentId, courseId);
                return Ok("Student enrolled in the course successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while enrolling student in course.");
            }
        }



       

    }
}
