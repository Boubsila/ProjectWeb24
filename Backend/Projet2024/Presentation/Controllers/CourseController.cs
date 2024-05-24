using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domaine;
using BusinessLayer;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // List of courses 'check'
        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Roles="1")]
        public Task< IEnumerable<Course> >Get()
        {
            return _courseService.GetAll();
        }


        //Course by Id
        [HttpGet("{courseId}")]
        [AllowAnonymous] 
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            try
            {
                var course = await _courseService.GetCourseById(courseId);
                if (course != null)
                {
                    return Ok(course); // Renvoie un code 200 (OK) avec le cours récupéré
                }
                else
                {
                    return NotFound(); // Renvoie un code 404 (Non trouvé) si le cours n'existe pas
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Renvoie un code 400 (Bad Request) en cas d'erreur
            }
        }



        //Add Course
        [HttpPost ("AddCourse")]
        //[Authorize(Roles = "1,2")]
        [AllowAnonymous]
        public async Task  Post( Course course)
        {
             _courseService.AddCourse(course);

        }

        
        
        // Endpoint update course
        [HttpPut("update/{courseId}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromBody] Course updatedCourse)
        {
            try
            {
                await _courseService.UpdateCourse(courseId, updatedCourse);
                return Ok("Course updated successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Endpoint delete course 
     
        [HttpDelete("delete/{courseId}")]
       // [Authorize(Roles = "1,2")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            try
            {
                await _courseService.DeleteCourse(courseId); // Attendre la méthode asynchrone
                return Ok("Course deleted successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Endpoint  Get student in course 'check'
        [HttpGet("student/{courseIdSt}")]
        //[Authorize(Roles = "1")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetStudentsInCourse(int courseIdSt)
        {
            try
            {
                var students = await _courseService.GetStudentsInCourse(courseIdSt);
                if (students == null)
                {
                    return NotFound(); // code 404
                }
                return Ok(students); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de la récupération des étudiants du cours : {ex.Message}");
               
            }
        }




        // Endpoint Get Instructor by Id course 'check'
        [HttpGet("{courseId}/Instructor")]
        //[Authorize(Roles = "1")]
        [AllowAnonymous]
     
        public async Task<ActionResult<IEnumerable<dynamic>>> GetInstructorsInCourse(int courseId)
        {
            try
            {
                var instructor = await _courseService.GetInstructorsInCourse(courseId);
                if (instructor == null)
                {
                    return NotFound(); // code 404
                }
                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de la récupération des instructeurs du cours : {ex.Message}");

            }
        }





        // Assing instructor to course 'check'
        [HttpPost("{courseId}/AssignInstructor")]
        //[Authorize(Roles = "1")]
        [AllowAnonymous]
        public async Task<IActionResult> AssignInstructorToCourse(int courseId, int instructorId)
        {
            try
            {
                await _courseService.AssignInstructorToCourse(courseId, instructorId);
                return Ok("Instructor assigned to course successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // Create/update Assignment For Course 'check' 
        [HttpPost("create-assignment")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAssignmentForCourse(int courseId, DateTime deadline)
        {
            try
            {
                // Assurez-vous que le cours existe
                var course = await _courseService.GetCourseById(courseId);
                if (course == null)
                {
                    return NotFound($"Course with ID {courseId} not found.");
                }

                // Enregistrez la date limite des devoirs pour ce cours
                await _courseService.UpdateAssignmentDeadlineForCourse(courseId, deadline);

                return Ok("Assignment deadline updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating assignment deadline: {ex.Message}");
            }
        }


        // Add Grade to student 

        [HttpPost("add-grade")]
        [AllowAnonymous]
        public async Task<IActionResult> AddGradeToStudentInCourse(int courseId, int userId, float grade)
        {
            try
            {
                
                await _courseService.AddGradeToStudentInCourse(courseId, userId, grade);

                
                return Ok("Grade added to student in course successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error adding grade to student in course: {ex.Message}");
            }
        }



    }
}
