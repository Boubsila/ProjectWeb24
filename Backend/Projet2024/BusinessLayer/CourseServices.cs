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
            _courseRepository = courseRepository;

        }


        // get list of course 'check '
        public Task<IEnumerable<Course>> GetAll()
        {
            return _courseRepository.GetAllCourses();
        }

        //Get course by Id 'check '
        public async Task<Course> GetCourseById(int courseId)
        {
            return await _courseRepository.GetCourseById(courseId);
        }


        //Add course 'check'
        public async  Task AddCourse(Course course)
        {
            _courseRepository.AddCourse(course);
        }

        //Update Course 
        public async Task UpdateCourse(int courseId, Course updatedCourse)
        {
            await _courseRepository.UpdateCourse(courseId, updatedCourse);
        }


        //delete course 'check'
        public async Task DeleteCourse(int courseId)
        {
            await _courseRepository.DeleteCourse(courseId); // Attendre la méthode asynchrone
        }


       

      
        public async Task<IEnumerable<dynamic>> GetStudentsInCourse(int courseId)
        {
          return await _courseRepository.GetStudentsInCourse(courseId);
        }


        
    }

}