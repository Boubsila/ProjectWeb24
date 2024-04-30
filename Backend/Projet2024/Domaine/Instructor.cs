using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Instructor : User
    {
        public ICollection<Course>? Courses { get; set; } // Liste des cours que l'instructeur enseigne
    }
}
