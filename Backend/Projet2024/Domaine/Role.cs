using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string? Name { get; set; }  // 'admin', 'instructeur', 'étudiant'
    }
}
