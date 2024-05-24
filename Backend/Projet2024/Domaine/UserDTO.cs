using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? Salt { get; set; }
        public string UserName { get; set; } // Non nullable
        public string Password { get; set; } // Non nullable
        public int RoleId { get; set; }




    }
}
