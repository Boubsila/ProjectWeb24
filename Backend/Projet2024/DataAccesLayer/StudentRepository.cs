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

        public async Task<IEnumerable<User>> GetAllStudents()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetStudentById(int studentId)
        {
            return await _context.Users.FindAsync(studentId);
        }

        public async Task AddStudent(User student)
        {
            await _context.Users.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudent(int studentId, User updatedStudent)
        {
            var existingStudent = await _context.Users.FindAsync(studentId);

            if (existingStudent != null)
            {
                existingStudent.Name = updatedStudent.Name;
                existingStudent.FirstName = updatedStudent.FirstName;
                existingStudent.RoleId = updatedStudent.RoleId;

                _context.Users.Update(existingStudent);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Student with ID {studentId} not found");
            }
        }

        public async Task DeleteStudent(int studentId)
        {
            var studentToDelete = await _context.Users.FindAsync(studentId);

            if (studentToDelete != null)
            {
                _context.Users.Remove(studentToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Student with ID {studentId} not found");
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesForStudent(int studentId)
        {
            //var student = await _context.users.Include(u => u.Courses).FirstOrDefaultAsync(u => u.UserId == studentId);
            //return student?.Courses;
            return null;
        }

        //public async Task EnrollStudentInCourse(int studentId, int courseId)
        //{
        //    var student = await _context.users.Include(u => u.Courses).FirstOrDefaultAsync(u => u.UserId == studentId);
        //    var courseToAdd = await _context.courses.FindAsync(courseId);

        //    if (student != null && courseToAdd != null)
        //    {
        //        if (student.Courses == null)
        //        {
        //            student.Courses = new Course[] { };
        //        }

        //        student.Courses.Append(courseToAdd);
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        throw new KeyNotFoundException($"Student or Course not found");
        //    }
        //}

        public async Task UnenrollStudentFromCourse(int studentId, int courseId)
        {
            //var student = await _context.users.Include(u => u.Course).FirstOrDefaultAsync(u => u.UserId == studentId);
            //var courseToRemove = student?.Course?.FirstOrDefault(c => c.CourseId == courseId);

            //if (student != null && courseToRemove != null)
            //{
            //    student.Course = student.Course.Except(new Course[] { courseToRemove });
            //    await _context.SaveChangesAsync();
            //}
            //else
            //{
            //    throw new KeyNotFoundException($"Student or Course not found");
            //}
        }

        public Task EnrollStudentInCourse(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }
    }
}
