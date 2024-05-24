using System.ComponentModel.DataAnnotations;

namespace Domaine
{
    public class Course
    {

       

        [Key]
        public int CourseId { get;}
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? AssignmentDeadline { get; set; }
        public ICollection<CourseUser>? CourseUsers { get; set; }

        public ICollection<CourseNote>? CourseNotes { get; set; }
    }
}