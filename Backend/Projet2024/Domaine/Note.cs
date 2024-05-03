using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
   public  class Note
    {
        [Key]
        public int NoteId { get; set; }
        public float Notes { get; set; }

        public IEnumerable<User>? User {  get; set; }
        public IEnumerable<Course>? Courses { get; set; }
        

    }
}
