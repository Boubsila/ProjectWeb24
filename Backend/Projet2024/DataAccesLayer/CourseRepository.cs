using Domaine;

namespace DataAccesLayer
{
    public class CourseRepository : ICourseRepository
    {
        private static List<Course> _db = new List<Course> { new Course(1,"web","Coure web"), new Course(2,"SGBD","Cours BD"), new Course(3,"Reseau","Cours de Reseau") };

        public void addCourse(Course course)
        {
            _db.Add(course);
        }

        public IEnumerable<Course> GetAll()
        {
            return _db;
        }

       
    }
}