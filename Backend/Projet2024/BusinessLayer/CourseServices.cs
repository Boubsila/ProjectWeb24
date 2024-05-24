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


        // Get list of course 'check '
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

        //Update Course 'Check'
        public async Task UpdateCourse(int courseId, Course updatedCourse)
        {
            await _courseRepository.UpdateCourse(courseId, updatedCourse);
        }


        //delete course 'check'
        public async Task DeleteCourse(int courseId)
        {
            await _courseRepository.DeleteCourse(courseId); // Attendre la méthode asynchrone
        }




        // Get student in course 'check'
        public async Task<IEnumerable<dynamic>> GetStudentsInCourse(int courseId)
        {
          return await _courseRepository.GetStudentsInCourse(courseId);
        }

        // Get Instructor in course 'check'
        public async Task<IEnumerable<dynamic>> GetInstructorsInCourse(int courseId)
        {
            return await _courseRepository.GetInstructorsInCourse(courseId);
        }



        // Assing instructor to course 'check'
        public async Task AssignInstructorToCourse(int courseId, int instructorId)
        {
             await _courseRepository.AssignInstructorToCourse(courseId, instructorId);
        }


        // Create/update Assignment For Course 'check'
        public async Task UpdateAssignmentDeadlineForCourse(int courseId, DateTime deadline)
        {
            await _courseRepository.UpdateAssignmentDeadlineForCourse(courseId, deadline);
        }


        //Add Grade to Student
        public async Task AddGradeToStudentInCourse(int courseId, int userId, float grade)
        {
          await _courseRepository.AddGradeToStudentInCourse(courseId, userId, grade);
        }






    }

}