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


        Task<IEnumerable<dynamic>> GetAllStudents();
        Task<dynamic> GetStudentById(int studentId);
        Task<IEnumerable<Course>> GetCoursesForStudent(int studentId);
        Task EnrollStudentInCourse(int studentId, int courseId);
    
    }
}
