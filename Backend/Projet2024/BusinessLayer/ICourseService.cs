using Domaine;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer
{
    public interface ICourseService
    {
        Task<  IEnumerable< Course>> GetAll();
        Task<Course> GetCourseById(int courseId);
        Task AddCourse(Course course);
        public  Task DeleteCourse(int courseId);
        public  Task UpdateCourse(int courseId, Course updatedCourse);

        public  Task<IEnumerable<dynamic>> GetStudentsInCourse(int courseId);
        public  Task<IEnumerable<dynamic>> GetInstructorsInCourse(int courseId);

        public Task AssignInstructorToCourse(int courseId, int instructorId);
        Task UpdateAssignmentDeadlineForCourse(int courseId, DateTime deadline);
        Task AddGradeToStudentInCourse(int courseId, int studentId, float grade);
    }
}