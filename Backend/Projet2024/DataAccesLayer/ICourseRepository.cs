using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domaine;

namespace DataAccesLayer
{
   public interface ICourseRepository
    {
      

        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourseById(int courseId);
        Task AddCourse(Course course);
        Task UpdateCourse(int courseId, Course updatedCourse);
        Task DeleteCourse(int courseId);
        Task<IEnumerable<dynamic>> GetStudentsInCourse(int courseId);
        Task<IEnumerable<dynamic>> GetInstructorsInCourse(int courseId);
        Task AssignInstructorToCourse(int courseId, int instructorId);
        Task<IEnumerable<CourseNote>> GetCourseGrades(int courseId);
        Task AddGradeToStudentInCourse(int courseId, int studentId, float grade);
    }
}
