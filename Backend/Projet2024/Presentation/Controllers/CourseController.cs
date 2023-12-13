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

        [HttpGet("{id}")]
        public Course GetById(int id)
        {
            return _courseService.GetCourseById(id);
        }

    }
}
