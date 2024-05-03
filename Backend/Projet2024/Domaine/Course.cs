using System.ComponentModel.DataAnnotations;

namespace Domaine
{
    public class Course
    {

       

        [Key]
        public int CourseId { get;}
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Note>? Notes { get; set; }

        public IEnumerable<User>? Users { get; set; }
    }
}