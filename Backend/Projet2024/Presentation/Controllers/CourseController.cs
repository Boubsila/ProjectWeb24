using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domaine;
using BusinessLayer;

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


        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return _courseService.GetAll();
        }

        [HttpPost]
        public void Post(Course course)
        {
            _courseService.addCourse(course);
        }

        // Endpoint update course
        [HttpPut("update/{courseId}")]
        public IActionResult UpdateCourse(int courseId, [FromBody] Course updatedCourse)
        {
            try
            {
                _courseService.UpdateCourse(courseId, updatedCourse);
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
        public IActionResult DeleteCourse(int  courseId)
        {
            try
            {
                _courseService.DeleteCourse(courseId);
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

    }
}
