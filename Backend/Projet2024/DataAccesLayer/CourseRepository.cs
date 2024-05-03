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
            return await _context.Courses.ToListAsync();
        }


        //Get Course by Id 'check'
        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }


        //Add Course 'check'
        public async Task AddCourse(Course course)
        {
           
                
                _context.Courses.Add(course);

               
                await _context.SaveChangesAsync();
          
        }

        
        
        //Update Course 'check'
        public async Task UpdateCourse(int courseId, Course updatedCourse)
        {
            var existingCourse = await _context.Courses.FindAsync(courseId);
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
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }



        // Get student in course 'check'

        public async Task<IEnumerable<dynamic>> GetStudentsInCourse(int courseId)
        {
            var students = await _context.Users
            .Where(u => _context.Courses.Any(c => c.CourseId == courseId) && u.Role.RoleId == 3)
            .Select(s => new { s.Name, s.FirstName, s.UserName })
            .ToListAsync();
           
            return students;
        }




        // Get Instructor in course 'check'
        public async Task<IEnumerable<dynamic>> GetInstructorsInCourse(int courseId)
        {
            var instructors = await _context.Users
                .Where(u => _context.Courses.Any(c => c.CourseId == courseId && u.Role.Name == "Instructor"))
                .Select(s => new { s.Name, s.FirstName, s.UserName })
                .ToListAsync();
            return instructors;
        }






        // Assing instructor to course 
        public async Task AssignInstructorToCourse(int courseId, int instructorId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            var instructor = await _context.Users.FindAsync(instructorId);

            if (course != null && instructor != null)
            {
                // Assurez-vous que l'utilisateur est un instructeur
                //if (instructor.Role.RoleId != 2)
                //{
                //    throw new InvalidOperationException("L'utilisateur spécifié n'est pas un professeur.");
                //}

                // Associez l'instructeur au cours en ajoutant l'instructeur à la liste des utilisateurs du cours
               // course.CourseUsers = new List<User> { instructor };

                await _context.SaveChangesAsync();
            }
        }






        public async Task<IEnumerable<CourseNote>> GetCourseGrades(int courseId)
        {
            //var course = await _context.courses.Include(c => c.Notes).FirstOrDefaultAsync(c => c.CourseId == courseId);
            //return course.Notes;
            return null;
        }

        public async Task AddGradeToStudentInCourse(int courseId, int studentId, float grade)
        {
            //var course = await _context.courses.Include(c => c.Notes).FirstOrDefaultAsync(c => c.CourseId == courseId);
            //if (course != null)
            //{
            //    //var student = course.Users.FirstOrDefault(u => u.UserId == studentId);
            //    //if (student != null)
            //    //{
            //    //    if (student.Notes == null)
            //    //    {
            //    //        student.Notes = new List<Note>();
            //    //    }
            //    //    student.Notes.Add(new Note { Notes = grade });
            //    //    await _context.SaveChangesAsync();
            //    //}
            //}
        }




    }
}