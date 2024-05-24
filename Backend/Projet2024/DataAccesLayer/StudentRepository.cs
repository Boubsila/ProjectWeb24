using Domaine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class StudentRepository:IStudentRepository
    {
        private readonly WebDbContext _context;

        public StudentRepository(WebDbContext context)
        {
            _context = context;
        }

     


        //Get All students 'check'
        public async Task<IEnumerable<dynamic>> GetAllStudents()
        {
           var students =  await _context.Users
                .Where(u => u.RoleId == 3)
                .Select(u => new
                {
                    u.UserId,
                    u.Name,
                    u.FirstName,
                    u.UserName
                })
                .ToListAsync();
            return students;
        }


        //Get student by Id 'Check'
        public async Task<dynamic> GetStudentById(int studentId)
        {
           var student =  await _context.Users
                .Where(u => u.UserId == studentId && u.RoleId == 3)
                .Select(u => new
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    FirstName = u.FirstName,
                    UserName = u.UserName
                })
                .FirstOrDefaultAsync();

            return student;
        }


        //Get Courses for student 'check'
        public async Task<IEnumerable<Course>> GetCoursesForStudent(int studentId)
        {
            var courses = await _context.Courses
                .Where(c => c.CourseUsers.Any(cu => cu.UserId == studentId) &&
                            _context.Users.Any(u => u.UserId == studentId && u.RoleId == 3))
                .ToListAsync();

            return courses;
        }


        //Enrolle Student in course 'check'
        public async Task EnrollStudentInCourse(int studentId, int courseId)
        {
            // Vérifier si l'étudiant existe
            var student = await _context.Users.FirstOrDefaultAsync(u => u.UserId == studentId && u.RoleId == 3);
            if (student == null)
            {
                throw new InvalidOperationException("L'utilisateur spécifié n'est pas un étudiant.");
            }

            // Vérifier si le cours existe
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                throw new InvalidOperationException("Le cours spécifié n'existe pas.");
            }

            // Vérifier si l'étudiant est déjà inscrit au cours
            var existingEnrollment = await _context.CourseUsers
                .FirstOrDefaultAsync(cu => cu.UserId == studentId && cu.CourseId == courseId);
            if (existingEnrollment != null)
            {
                throw new InvalidOperationException("L'étudiant est déjà inscrit à ce cours.");
            }

            // Créer une nouvelle inscription pour l'étudiant au cours
            var newEnrollment = new CourseUser { UserId = studentId, CourseId = courseId };
            _context.CourseUsers.Add(newEnrollment);
            await _context.SaveChangesAsync();
        }




        




    }
}
