using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Student : User
    {
        public ICollection<Course>? Courses { get; set; }  // Liste des cours auxquels l'étudiant est inscrit
        public float Grade { get; set; } // Note de l'étudiant
    }
}
