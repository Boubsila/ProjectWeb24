using DataAccesLayer;
using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class StudentService : IStudentService
    {


        private readonly IStudentRepository _studentSrv;

        public StudentService(IStudentRepository studentSrv) 
        {
            _studentSrv = studentSrv;
        }

        //Get All Students 

        public async Task<IEnumerable<dynamic>> GetAllStudents()
        {
           return await _studentSrv.GetAllStudents();
        }


        // Get student by id 'check'

        public async Task<dynamic> GetStudentById(int studentId)
        {
            return await _studentSrv.GetStudentById(studentId);
        }


        //Get Course for student 'check'
        public async Task<IEnumerable<Course>> GetCoursesForStudent(int studentId)
        {
            return await _studentSrv.GetCoursesForStudent(studentId);
        }

        
        // Enroll Student in course 'check'
        public async Task EnrollStudentInCourse(int studentId, int courseId)
        {
            await _studentSrv.EnrollStudentInCourse(studentId, courseId);
        }






       

       
    }
}
