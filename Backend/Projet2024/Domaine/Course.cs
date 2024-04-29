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

    }
}