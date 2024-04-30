namespace Domaine
{
    public class Course
    {
        public Course(int Id,string Name,string Description) 
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
        }
        public int Id { get;}
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Student> Students { get; set; } // Liste des étudiants inscrits
        public ICollection<Instructor> Instructors { get; set; } // Liste des instructeurs assignés

    }
}