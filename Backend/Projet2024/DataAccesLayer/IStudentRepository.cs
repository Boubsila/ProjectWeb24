using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public  interface IStudentRepository
    {
        Task<IEnumerable<User>> GetAllStudents();
        Task<User> GetStudentById(int studentId);
        Task AddStudent(User student);
        Task UpdateStudent(int studentId, User updatedStudent);
        Task DeleteStudent(int studentId);
        Task<IEnumerable<Course>> GetCoursesForStudent(int studentId);
        Task EnrollStudentInCourse(int studentId, int courseId);
        Task UnenrollStudentFromCourse(int studentId, int courseId);
    }
}
