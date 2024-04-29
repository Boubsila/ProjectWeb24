using Domaine;

namespace DataAccesLayer
{
    public class CourseRepository : ICourseRepository
    {
        //DB temp to replace with the Data base
        private static List<Course> _db = new List<Course> { new Course(1,"web","Coure web"), new Course(2,"SGBD","Cours BD"), new Course(3,"Reseau","Cours de Reseau") };


        // 1.1.2 Course Management : Create a course (Dot 1/4)
        public void addCourse(Course course)
        {
            if (_db.Any(c => c.Id == course.Id || c.Name == course.Name || c.Description == course.Description))
            {
                throw new ArgumentException($"Course with ID {course.Id} already exists");
            }
                _db.Add(course);
        }

        // 1.1.2 Course Management : Update a course (Dot 2/4)
        public void UpdateCourse(int courseId, Course updatedCourse)
        {
            var existingCourse = _db.FirstOrDefault(course => course.Id == courseId);

            if (existingCourse != null)
            {
               
                existingCourse.Name = updatedCourse.Name;
                existingCourse.Description = updatedCourse.Description;
            }
            else
            {
                throw new ArgumentException($"Course with ID {courseId} not found");
            }
        }

        // 1.1.2 Course Management : delete a course (Dot 3/4)
        public void DeleteCourse(int courseId)
        {
            var courseToRemove = _db.FirstOrDefault(courses => courses.Id == courseId);

            if (courseToRemove != null)
            {
                _db.Remove(courseToRemove);
            }
            else
            {
                throw new ArgumentException($"Course with ID {courseId} not found");
            }
        }

        // 1.1.2 Course Management : List all available course (Dot 4/4)
        public IEnumerable<Course> GetAll()
        {
            return _db;
        }

       
    }
}