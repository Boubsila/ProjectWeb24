using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }

        public string? Salt { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; } 
        public int RoleId { get; set; } // Référence à l'ID du rôle
        public Role Role { get; set; } // Le rôle de l'utilisateur
        public IEnumerable<Course>? Courses { get; set;}
    
    }
}
