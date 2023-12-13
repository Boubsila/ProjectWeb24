using DataAccesLayer;
using Domaine;
using Hl7.Fhir.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer
{
    public class CourseServices : ICourseService
    {
       private readonly ICourseRepository _courseRepository ;

        public CourseServices(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ;
        }
        
        public IEnumerable<Course> GetAll() 
        {
            return _courseRepository.GetAll();
        }

        public void addCourse(Course course) 
        {
            _courseRepository.addCourse(course);
        }

        public Course GetCourseById(int id)
        {
            
            Course course = _courseRepository.GetAll().FirstOrDefault(c => c.Id == id);

            if (course != null)
            {
                return course;
            }
            else
            {
                //Temp Solution
                Course c = new Course(id, "null", "null");
                return c;
            }

          
        }

    }

}