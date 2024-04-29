using DataAccesLayer;
using Domaine;
using Microsoft.AspNetCore.Mvc;


namespace BusinessLayer
{
    public class CourseServices : ICourseService
    {
       private readonly ICourseRepository _courseRepository ;
       
        //init interface
        public CourseServices(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ;
            
        }
     

        // 1.1.2 Course Management : Create a course (Dot 1/4)
        public void addCourse(Course course) 
        {
            _courseRepository.addCourse(course);
        }

        // 1.1.2 Course Management : Update a course (Dot 2/4)

        public void UpdateCourse(int courseId, Course updatedCourse)
        {
            var existingCourse = _courseRepository.GetAll().FirstOrDefault(course => course.Id == courseId);

            if (existingCourse != null)
            {
                existingCourse.Name = updatedCourse.Name;
                existingCourse.Description = updatedCourse.Description;
            }
            else
            {
                throw new ArgumentException($"Course with ID {courseId} not found");
            }
        }

        // 1.1.2 Course Management : delete a course (Dot 3/4)
        public void DeleteCourse(int courseId)
        {
           
            var courseToRemove = _courseRepository.GetAll().FirstOrDefault(courses => courses.Id == courseId);

            if (courseToRemove != null)
            {
                _courseRepository.DeleteCourse(courseId);
            }
            else
            {
                throw new ArgumentException($"Course with ID {courseId} not found");
            }
        }

        // 1.1.2 Course Management : List all available course (Dot 4/4)
        public IEnumerable<Course> GetAll()
        {
            return _courseRepository.GetAll();
        }
    }

}