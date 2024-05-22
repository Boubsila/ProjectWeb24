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
            var students = await _context.CourseUsers
                .Where(cu => cu.CourseId == courseId) // Filtre par ID de cours
                .Where(cu => cu.User.RoleId == 3) // Filtre pour les étudiants (RoleId = 3)
                .Select(cu => new { cu.User.Name, cu.User.FirstName, cu.User.UserName })
                .ToListAsync();

            return students;
        }




        // Get Instructor in course 'check'
        public async Task<IEnumerable<dynamic>> GetInstructorsInCourse(int courseId)
        {
            var instructors = await _context.CourseUsers
                .Where(cu => cu.CourseId == courseId) // Filtre par ID de cours
                .Where(cu => cu.User.RoleId == 2)
                .Select(cu => new { cu.User.Name, cu.User.FirstName, cu.User.UserName })
                .ToListAsync();
            return instructors;
        }


        // Assing instructor to course 'check'
        public async Task AssignInstructorToCourse(int courseId, int instructorId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            var instructor = await _context.Users.FindAsync(instructorId);

            if (course != null && instructor != null)
            {
                // Vérifier si l'utilisateur est déjà associé au cours en tant qu'instructeur
                var existingAssignment = await _context.CourseUsers
                    .FirstOrDefaultAsync(cu => cu.CourseId == courseId && cu.UserId == instructorId);

                if (existingAssignment != null)
                {
                    // L'utilisateur est déjà un instructeur pour ce cours
                    return;
                }

                // Créer une nouvelle entrée dans la table de liaison CourseUser
                var courseUser = new CourseUser
                {
                    CourseId = courseId,
                    UserId = instructorId
                };

                _context.CourseUsers.Add(courseUser);

                await _context.SaveChangesAsync();
            }
        }


        // Create/update Assignment For Course 'check'
        public async Task UpdateAssignmentDeadlineForCourse(int courseId, DateTime deadline)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                course.AssignmentDeadline = deadline;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");
            }
        }



        // Add Grade to student 'check'
        public async Task AddGradeToStudentInCourse(int courseId, int userId, float grade)
        {
            try
            {
                // Vérifie si le cours existe
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                {
                    throw new KeyNotFoundException($"Le cours avec l'ID {courseId} n'a pas été trouvé.");
                }

                // Vérifie si l'utilisateur est un étudiant (roleId = 3)
                var student = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == 3);
                if (student == null)
                {
                    throw new InvalidOperationException($"L'utilisateur avec l'ID {userId} n'est pas un étudiant.");
                }

                // Vérifie si l'étudiant est inscrit au cours
                var courseUser = await _context.CourseUsers
                    .FirstOrDefaultAsync(cu => cu.CourseId == courseId && cu.UserId == userId);
                if (courseUser == null)
                {
                    throw new InvalidOperationException($"L'étudiant avec l'ID {userId} n'est pas inscrit au cours avec l'ID {courseId}.");
                }

                // Crée une nouvelle note pour l'étudiant dans le cours
                var courseNote = new CourseNote
                {
                    CourseId = courseId,
                    UserId = userId,
                    Note = grade
                };

                _context.CourseNotes.Add(courseNote);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de l'ajout de la note à l'étudiant dans le cours : {ex.Message}");
            }
        }









    }
}