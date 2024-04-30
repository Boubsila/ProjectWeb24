using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } // Référence à l'ID du rôle
        public Role Role { get; set; } // Le rôle de l'utilisateur
    }
}
