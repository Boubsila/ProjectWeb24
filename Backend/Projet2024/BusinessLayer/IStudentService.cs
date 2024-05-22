using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IStudentService
    {
        Task<IEnumerable<dynamic>> GetAllStudents();
        Task<dynamic> GetStudentById(int studentId);

        Task<IEnumerable<Course>> GetCoursesForStudent(int studentId);
        Task EnrollStudentInCourse(int studentId, int courseId);
    }
}
