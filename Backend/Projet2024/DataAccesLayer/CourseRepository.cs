using Domaine ;
using Microsoft.EntityFrameworkCore;


namespace DataAccesLayer
{
    public class CourseRepository : ICourseRepository
    {
        
        private readonly WebDbContext _context;

        public CourseRepository(WebDbContext context)
        {
            _context = context;
        }

        //List of Course 'course'
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _context.courses.ToListAsync();
        }


        //Get Course by Id 'check'
        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.courses.FindAsync(courseId);
        }


        //Add Course 'check'
        public async Task AddCourse(Course course)
        {
           
                
                _context.courses.Add(course);

               
                await _context.SaveChangesAsync();
          
        }

        
        
        //Update Course 'check'
        public async Task UpdateCourse(int courseId, Course updatedCourse)
        {
            var existingCourse = await _context.courses.FindAsync(courseId);
            if (existingCourse != null)
            {
                existingCourse.Name = updatedCourse.Name;
                existingCourse.Description = updatedCourse.Description;

                try {
                    await _context.SaveChangesAsync();
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
             }
        }


        //Delete Course 'check'
        public async Task DeleteCourse(int courseId)
        {
            var course = await _context.courses.FindAsync(courseId);
            if (course != null)
            {
                _context.courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }



        // Get student in course 

        public async Task<IEnumerable<dynamic>> GetStudentsInCourse(int courseId)
        {
            var students = await _context.users
            .Where(u => _context.courses.Any(c => c.CourseId == courseId) && u.Role.RoleId == 3)
            .Select(s => new { s.Name, s.FirstName, s.UserName })
            .ToListAsync();

            return students;
        }





        public async Task<IEnumerable<User>> GetInstructorsInCourse(int courseId)
        {
            var instructors = await _context.users.Where(u => u.Courses.Any(c => c.CourseId == courseId && u.Role.Name == "Instructor")).ToListAsync();
            return instructors;
        }

        public async Task AssignInstructorToCourse(int courseId, int instructorId)
        {
            var course = await _context.courses.FindAsync(courseId);
            var instructor = await _context.users.FindAsync(instructorId);

            if (course != null && instructor != null)
            {
                // Assurez-vous que l'utilisateur est un instructeur
                if (instructor.Role.RoleId != 2)
                {
                    throw new InvalidOperationException("L'utilisateur spécifié n'est pas un professeur.");
                }

                // Associez l'instructeur au cours en ajoutant l'instructeur au cours
                //course.Users = instructor;

                await _context.SaveChangesAsync();
            }
        }




        public async Task<IEnumerable<Note>> GetCourseGrades(int courseId)
        {
            var course = await _context.courses.Include(c => c.Notes).FirstOrDefaultAsync(c => c.CourseId == courseId);
            return course.Notes;
        }

        public async Task AddGradeToStudentInCourse(int courseId, int studentId, float grade)
        {
            var course = await _context.courses.Include(c => c.Notes).FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (course != null)
            {
                //var student = course.Users.FirstOrDefault(u => u.UserId == studentId);
                //if (student != null)
                //{
                //    if (student.Notes == null)
                //    {
                //        student.Notes = new List<Note>();
                //    }
                //    student.Notes.Add(new Note { Notes = grade });
                //    await _context.SaveChangesAsync();
                //}
            }
        }




    }
}